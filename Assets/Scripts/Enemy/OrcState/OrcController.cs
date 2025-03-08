using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcController : EnemyController, IDamageable
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

    void Update()
    {
        currentState.UpdateState(this);
    }
    
    public void TransitionToState(IEnemyState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }


    public void TakeDamage(float amount)
    { 
        lastState = currentState;
        currentLife -= amount; 

        if(currentLife <= 0)
        {
            TransitionToState(deathState);
            return;
        }

        TransitionToState(hurtState);
    }
}
