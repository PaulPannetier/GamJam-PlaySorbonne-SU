using UnityEngine;

public class OrcController : EnemyController
{
    [Header("OrcState")]
    public OrcIdleState idleState ; 
    public OrcPatrolState patrolState;
    public OrcChaseState chaseState ;
    public OrcAttackState attackState ;
    
    protected override void Start()
    {
        base.Start();

        currentState = idleState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
