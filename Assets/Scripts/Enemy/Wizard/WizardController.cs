using UnityEngine;
using Pathfinding;
using System.Collections;
using System;

public class WizardController :  EnemyController , IDamageable
{
    [Header("State")]    
    public WizardIdleState idleState; 
    public WizardPatrolState patrolState;
    public WizardChaseState chaseState ;
    public WizardAttackState attackState ;
    /*
    public StuntState stuntState { get; private set; } = new StuntState();
    */
    
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
        Debug.Log("touergqe");
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
