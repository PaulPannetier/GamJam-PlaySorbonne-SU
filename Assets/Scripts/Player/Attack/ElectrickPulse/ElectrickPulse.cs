using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;
using System.Collections.Generic;

public class ElectrickPulse : MonoBehaviour
{
    private Animator animator;
    private ElectrickPulseAttack playerAttack;
    private PlayerFightController playerFightController;
    private float currentAngle; //in deg
    private float angularSpeed;
    private float rotRadius;
    private float duration;
    private List<EnemyController> enemyAlreadyTouch;

    [SerializeField] private string startAnimName;
    [SerializeField] private string idleAnimName;
    [SerializeField] private string endAnimName;
    [SerializeField] private Vector2 collisionOffset;
    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private bool drawGizmos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        float length;
        if(animator.GetAnimationLength(startAnimName, out length))
        {
            this.Invoke(() => { animator.CrossFade(idleAnimName, 0f, 0); }, length);
        }
        else
        {
            animator.CrossFade(startAnimName, 0f, 0);
        }

        if(animator.GetAnimationLength(endAnimName, out length))
        {
            this.Invoke(() => { animator.CrossFade(endAnimName, 0f, 0); }, Mathf.Max(0f, duration - length));
        }

        enemyAlreadyTouch = new List<EnemyController>();
    }

    public void Launch(ElectrickPulseAttack playerAttack, PlayerFightController playerFightController, float radius, float angularSpeed, float duration)
    {
        this.playerAttack = playerAttack;
        this.rotRadius = radius;
        this.angularSpeed = angularSpeed;
        this.duration = duration;
        this.playerFightController = playerFightController;
        currentAngle = Useful.AngleHori(playerAttack.transform.position, transform.position) * Mathf.Rad2Deg;
        this.Invoke(() => Destroy(gameObject), duration);
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position + collisionOffset, collisionRadius, enemyMask);
        foreach (Collider2D col in cols)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();
            if (enemyController != null && !enemyAlreadyTouch.Contains(enemyController))
            {
                playerAttack.OnElectrickPulseTouchEnemy(this, enemyController);
                enemyAlreadyTouch.Add(enemyController);
            }
        }

        currentAngle += angularSpeed * Time.deltaTime;
        currentAngle %= 360f;
        Vector2 offset = Useful.Vector2FromAngle(currentAngle * Mathf.Deg2Rad, rotRadius);
        transform.position = (Vector2)playerFightController.transform.position + offset;

        Vector2 normal = offset.NormalVector();
        Vector2 offset2 = Useful.Vector2FromAngle((currentAngle + 1f) * Mathf.Deg2Rad, rotRadius);
        if (normal.Dot(offset2 - offset) > 0f)
            normal = -normal;

        transform.rotation = Quaternion.Euler(0f, 0f, Useful.AngleHori(Vector2.zero, normal) * Mathf.Rad2Deg);
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
            return;

        Circle.GizmosDraw((Vector2)transform.position + collisionOffset, collisionRadius, Color.red);
    }

    private void OnValidate()
    {
        collisionRadius = Mathf.Max(0f, collisionRadius);
        angularSpeed = Mathf.Max(0f, angularSpeed);
    }
}
