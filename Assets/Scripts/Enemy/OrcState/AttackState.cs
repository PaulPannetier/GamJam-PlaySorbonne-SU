using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class OrcAttackState : IEnemyState
{
    [SerializeField] private List<Spell> spells = new List<Spell>();
    [SerializeField] private float attackRange = 2f;
    
    [SerializeField] public Transform target;

    private List<Spell> availableSpell = new List<Spell>();
    private float attackCooldownTimer = 0f;

    private bool canAttack = true;


    public void EnterState(EnemyController enemy)
    {
        OrcController orcController = (OrcController)enemy;
        enemy.animator.SetBool("isAttack", true);
        UpdateAvailableSpell(enemy);

        if (availableSpell.Count < 1)
        {
            Debug.Log("pas d'attaque disponible");
            orcController.TransitionToState(orcController.chaseState);
        }
    }

    public void UpdateState(EnemyController enemy)
    {
        OrcController orcController = (OrcController)enemy;
        if (canAttack)
        {
            UpdateTarget(enemy);
            if(target == null)
            {
                orcController.TransitionToState(orcController.chaseState);
                return;
            }

            UpdateAvailableSpell(enemy);
            if (availableSpell.Count < 1)
            {
                Debug.Log("pas d'attaque disponible");
                orcController.TransitionToState(orcController.chaseState);
                return;
            }

            Spell nextSpell = availableSpell.GetRandom();
            nextSpell.Cast(enemy);
            StartCooldown(nextSpell.cooldown);
        }

        if (!canAttack)
        {
            attackCooldownTimer -= Time.deltaTime;
            if (attackCooldownTimer <= 0f)
            {
                canAttack = true;
            }
        }
    }

    public void ExitState(EnemyController enemy)
    {
        enemy.animator.SetBool("isAttack", false);

    }

    void UpdateAvailableSpell(EnemyController enemy)
    {
        availableSpell.Clear();

        foreach (Spell spell in spells)
        {
            if (spell.Condition(enemy))
            {
                availableSpell.Add(spell);
            }
        }
    }

    void StartCooldown(float cooldown)
    {
        canAttack = false;
        attackCooldownTimer = cooldown;
    }

    
    void UpdateTarget(EnemyController enemy)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(enemy.transform.position, attackRange, Vector2.zero);

        List<Transform> playersInRange = new List<Transform>();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                playersInRange.Add(hit.transform);
            }
        }

        target = GetClosestPlayer(enemy.transform.position, playersInRange);
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
}