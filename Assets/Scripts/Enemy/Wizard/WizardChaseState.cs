using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System.Collections;
using UnityEditorInternal;

[System.Serializable]
public class WizardChaseState : IEnemyState
{
    [SerializeField] private float maxDetectionDistance = 20f;
    [SerializeField] private float chaseDistance = 10f;
    [SerializeField] private float minChaseDuration = 1f;

    [SerializeField] public Transform target;

    [SerializeField] private float speed = 120f;

    // Distance à laquelle l'ennemi considère qu'il a atteint un waypoint
    [SerializeField] private float nextWpDistance = 1f;

    // Distance minimale pour déclencher une attaque (l'ennemi s'arrête en dehors de cette portée)
    [SerializeField] private float attackRange = 2f;

    [SerializeField] private float attackStateCooldown = 0.2f;
    //private bool canTransitionToAttackState = false;
    private bool canStopChase = false;


    int currWp = 0;

    private Seeker seeker;
    private Path path;
    private Rigidbody2D rb;
    private Coroutine updateTargetCoroutine;
    private Coroutine updatePathCoroutine;

    public void EnterState(EnemyController enemy)
    {
        WizardController wizardController = (WizardController)enemy;
        seeker = enemy.seeker;
        rb = enemy.rb;

        canStopChase = false;

        updateTargetCoroutine = enemy.StartCoroutine(UpdateTargetRoutine(enemy));
        updatePathCoroutine = enemy.StartCoroutine(UpdatePathRoutine(enemy));
        enemy.Callback(CanStopChase, minChaseDuration);

    }

    private IEnumerator UpdateTargetRoutine(EnemyController enemy)
    {
        while (true)
        {
            target = UpdateTarget(enemy);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator UpdatePathRoutine(EnemyController enemy)
    {
        while (true)
        {
            UpdatePath();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateState(EnemyController enemy)
    {
        WizardController wizardController = (WizardController)enemy;
        if (target == null)
        {
            target = UpdateTarget(enemy);
            if (target == null)
            {
                enemy.TransitionToState(wizardController.patrolState);
                return;
            }
        }

        if (path == null || currWp >= path.vectorPath.Count)
        {
            return;
        }

        // Calcule la distance entre l'ennemi et le joueur
        float playerDistance = Vector2.Distance(target.transform.position, enemy.transform.position);

        if (playerDistance > chaseDistance && canStopChase)
        {
            enemy.TransitionToState(wizardController.patrolState);
        }

        // Si le joueur est en dehors de la portée d'attaque = on le poursuit
        if (playerDistance > attackRange)
        {
            // Calcule la direction vers le prochain waypoint
            Vector2 direction = ((Vector2)path.vectorPath[currWp] - rb.position).normalized;

            // Lisser la direction pour éviter des changements brusques (interpolation)
            Vector2 smoothDirection = Vector2.Lerp(rb.linearVelocity.normalized, direction, 0.1f);

            // Calcule la vitesse en fonction de la direction et de la vitesse spécifiée
            Vector2 velocity = smoothDirection * speed * Time.fixedDeltaTime;

            // Applique la vitesse calculée au Rigidbody2D
            rb.linearVelocity = velocity;

            enemy.animator.SetFloat("Speed", velocity.sqrMagnitude);

            // Calcule la distance entre l'ennemi et le waypoint actuel
            float distance = Vector2.Distance(rb.position, path.vectorPath[currWp]);

            // Si l'ennemi est suffisamment proche du waypoint, passe au suivant
            if (distance < nextWpDistance)
            {
                currWp++;
            }

            if (direction.x > 1e-3)
            {
                enemy.spriteRenderer.flipX = direction.x < 0;
            }
        }

        if (playerDistance < attackRange)
        {
            enemy.TransitionToState(wizardController.attackState);
        }


    }


    public void ExitState(EnemyController enemy)
    {
        if (updateTargetCoroutine != null)
            enemy.StopCoroutine(updateTargetCoroutine);
        if (updatePathCoroutine != null)
            enemy.StopCoroutine(updatePathCoroutine);
    }


    Transform UpdateTarget(EnemyController enemy)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(enemy.transform.position, maxDetectionDistance, Vector2.zero);

        List<Transform> playersInRange = new List<Transform>();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                playersInRange.Add(hit.transform);
            }
        }

        return GetClosestPlayer(enemy.transform.position, playersInRange);
    }

    public Transform GetClosestPlayer(Vector2 currentPosition, List<Transform> playersInRange)
    {
        if (playersInRange == null || playersInRange.Count == 0)
            return null; // Aucun joueur dans la liste

        Transform closestPlayer = null;
        float closestDistanceSqr = float.MaxValue; // Distance la plus courte trouvée

        foreach (Transform player in playersInRange)
        {
            float distanceSqr = (player.position - (Vector3)currentPosition).sqrMagnitude; // Distance au carré
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }

    void UpdatePath()
    {
        // Vérifie si le Seeker est prêt à calculer un nouveau chemin
        if (seeker.IsDone())
            // Demande un nouveau chemin du Seeker entre la position actuelle et la cible
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        // Si le calcul a réussi (pas d'erreur), met à jour le chemin et réinitialise l'indice du waypoint
        if (!p.error)
        {
            path = p;
            currWp = 0;
        }
    }

    void CanStopChase()
    {
        canStopChase = true;
    }
}
