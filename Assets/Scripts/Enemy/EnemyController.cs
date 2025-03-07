using UnityEngine;
using Pathfinding;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour
{
    
    [Header("State")]
    public IEnemyState currentState;
    public IdleState idleState ; 
    public PatrolState patrolState;
    public ChaseState chaseState ;
    public AttackState attackState ;
    /*
    public DeathState deathState { get; private set; } = new DeathState();
    public StuntState stuntState { get; private set; } = new StuntState();
    */

    [Header("Component")]
    
    [HideInInspector]public Seeker seeker;
    [HideInInspector]public Rigidbody2D rb;

    public Vector2 startPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = idleState;
        startPosition = transform.position;
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

    public IEnumerator Callback(Action callback,float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        callback();
    }
}
