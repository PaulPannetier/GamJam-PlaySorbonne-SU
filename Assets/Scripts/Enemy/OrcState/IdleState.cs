using UnityEngine;


[System.Serializable]
public class OrcIdleState : IEnemyState
{
    [SerializeField] private float activationRange = 20f;
    public void EnterState(EnemyController enemy)
    {
        OrcController orcController = (OrcController)enemy;
        orcController.TransitionToState(orcController.patrolState);
    }

    public void UpdateState(EnemyController enemy)
    {
        OrcController orcController = (OrcController)enemy;
        if (CheckPlayerInRange(enemy))
        {
            orcController.TransitionToState(orcController.patrolState);
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

