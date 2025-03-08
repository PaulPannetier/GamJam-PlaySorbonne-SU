using UnityEngine;


[System.Serializable]
public class WizardIdleState : IEnemyState
{
    [SerializeField] private float activationRange = 20f;
    public void EnterState(EnemyController enemy)
    {
        WizardController wizardController = (WizardController)enemy;
        wizardController.TransitionToState(wizardController.patrolState);
    }

    public void UpdateState(EnemyController enemy)
    {
        WizardController wizardController = (WizardController)enemy;
        if (CheckPlayerInRange(enemy))
        {
            wizardController.TransitionToState(wizardController.patrolState);
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

