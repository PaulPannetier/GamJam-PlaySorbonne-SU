using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;

[System.Serializable]
public class PatrolState : IEnemyState
{
    [SerializeField] private float ChasePlayerRange = 10f;
    [SerializeField] private float speed = 120f;
    [SerializeField] private float nextWpDistance = 1f;
    [SerializeField] private Vector2 timeRand = new Vector2(0.5f, 4f);
    [SerializeField] private Vector2 randDistance = new Vector2(0.5f, 2f);

    private Transform target;


    int currWp = 0;
    private Seeker seeker;
    private Path path;
    private Rigidbody2D rb;

    private Coroutine detectPlayerCoroutine;
    private Coroutine MoveToRandomPointCoroutine;

    public void EnterState(EnemyController enemy)
    {

        seeker = enemy.seeker;
        rb = enemy.rb;

        detectPlayerCoroutine = enemy.StartCoroutine(DetectPlayer(enemy));
        MoveToRandomPointCoroutine = enemy.StartCoroutine(GetNewPoint(enemy));
    }

    public void UpdateState(EnemyController enemy)
    {
        //poursuit le joueur si possible 
        if (target != null)
        {
            enemy.TransitionToState(enemy.chaseState);
        }

        //update la position 

        if (path == null || currWp >= path.vectorPath.Count)
        {
            enemy.animator.SetFloat("Speed", 0);
            return;
        }

        // Calcule la direction vers le prochain waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currWp] - rb.position).normalized;
        Vector2 smoothDirection = Vector2.Lerp(rb.linearVelocity.normalized, direction, 0.1f);
        Vector2 velocity = smoothDirection * speed * Time.fixedDeltaTime;
        rb.linearVelocity = velocity;
        
        if (direction.x > 1e-3)
        {
            enemy.spriteRenderer.flipX = direction.x < 0;
        }

        enemy.animator.SetFloat("Speed", velocity.sqrMagnitude);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currWp]);

        if (distance < nextWpDistance)
        {
            currWp++;
        }
    }


    public void ExitState(EnemyController enemy)
    {
        if (detectPlayerCoroutine != null)
            enemy.StopCoroutine(detectPlayerCoroutine);

        if (MoveToRandomPointCoroutine != null)
            enemy.StopCoroutine(MoveToRandomPointCoroutine);
    }

    #region UpdateTarget
    private IEnumerator DetectPlayer(EnemyController enemy)
    {
        while (true)
        {
            target = UpdateTarget(enemy);
            yield return new WaitForSeconds(0.5f);
        }
    }

    Transform UpdateTarget(EnemyController enemy)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(enemy.transform.position, ChasePlayerRange, Vector2.zero);

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

    #endregion

    #region Path

    private IEnumerator GetNewPoint(EnemyController enemy)
    {
        while (true)
        {
            UpdatePath(enemy);
            yield return new WaitForSeconds(Random.Rand(timeRand.x, timeRand.y));
        }
    }
    void UpdatePath(EnemyController enemy)
    {
        // Vérifie si le Seeker est prêt à calculer un nouveau chemin
        if (seeker.IsDone())
        {
            Vector2 targetPosition = (Vector2)enemy.transform.position + Random.PointInCircle(enemy.transform.position, randDistance.y);
            // Demande un nouveau chemin du Seeker entre la position actuelle et la cible
            seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        }
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
    #endregion

}