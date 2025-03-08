using UnityEngine;


[System.Serializable]
public class IdleState : IEnemyState
{
    [SerializeField] private float activationRange = 20f;
    public void EnterState(EnemyController enemy)
    {
        enemy.TransitionToState(enemy.patrolState);
    }

    public void UpdateState(EnemyController enemy)
    {
        if (CheckPlayerInRange(enemy))
        {
            enemy.TransitionToState(enemy.patrolState);
        }
    }

    public void ExitState(EnemyController enemy)
    {

    }

    bool CheckPlayerInRange(EnemyController enemy)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(enemy.transform.position, activationRange, enemy.transform.forward);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}

