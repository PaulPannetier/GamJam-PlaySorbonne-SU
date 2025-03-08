using UnityEngine;
using Pathfinding;
using System.Collections;
using System;

public class WizardController :  EnemyController
{
    [Header("State")]    
    public WizardIdleState idleState ; 
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
}
