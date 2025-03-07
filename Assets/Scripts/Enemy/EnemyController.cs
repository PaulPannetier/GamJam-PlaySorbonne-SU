using UnityEngine;
using Pathfinding;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour, IDamageable
{
    
    [Header("State")]
    public IEnemyState currentState;
    public IdleState idleState ; 
    public PatrolState patrolState;
    public ChaseState chaseState ;
    public AttackState attackState ;
    public HurtState hurtState;
    public DeathState deathState;
    /*
    public StuntState stuntState { get; private set; } = new StuntState();
    */

    [Header("LifeSystem")]
    [SerializeField] private float maxLife = 10f;
    [SerializeField] private float currentLife;


    [Header("Component")]
    
    [HideInInspector]public Seeker seeker;
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public Animator animator;

    public Vector2 startPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = idleState;
        startPosition = transform.position;

        currentLife = maxLife;
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

    public void TakeDamage(float amount)
    { 
        currentLife -= amount; 

        if(currentLife <= 0)
        {
            TransitionToState(deathState);
            
        }
    }
}
