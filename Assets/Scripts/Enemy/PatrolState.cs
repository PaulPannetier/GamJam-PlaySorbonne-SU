using UnityEngine;

[System.Serializable]
public class PatrolState : IEnemyState
{
    [SerializeField] private float activationRange = 20f;
    public void EnterState(EnemyController enemy)
    {

    }

    public void UpdateState(EnemyController enemy)
    {

    }

    public void ExitState(EnemyController enemy)
    {

    }

}