using UnityEngine;
using Pathfinding;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour
{
    [Header("State")]
    public IEnemyState currentState;
    public IEnemyState lastState;
    public HurtState hurtState;
    public DeathState deathState;
    /*
    public StuntState stuntState { get; private set; } = new StuntState();
    */

    [Header("LifeSystem")]
    [SerializeField] public float maxLife = 10f;
    [SerializeField] public float currentLife;


    [Header("Component")]
    
    [HideInInspector]public Seeker seeker;
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        currentLife = maxLife;
    }

    
    public void BasicTransitionToState(IEnemyState newState)
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
