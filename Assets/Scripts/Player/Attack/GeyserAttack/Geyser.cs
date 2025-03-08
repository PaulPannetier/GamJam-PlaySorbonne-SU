using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;

public class Geyser : MonoBehaviour
{
    private GeyserAttack geyserAttack;
    private Animator animator;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float duration;
    [SerializeField] private string idleAnimationName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Launch(GeyserAttack geyserAttack)
    { 
        this.geyserAttack = geyserAttack;
        if(animator.GetAnimationLength(idleAnimationName, out float length))
        {
            animator.speed = length / duration;
        }
        this.Invoke(() => Destroy(gameObject), duration);
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position, radius, enemyMask);
        foreach (Collider2D col in cols)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                geyserAttack.OnGeyserTouch(this, enemyController);
            }
        }
    }

    private void OnValidate()
    {
        radius = Mathf.Max(radius, 0f);
        duration = Mathf.Max(duration, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Circle.GizmosDraw(transform.position, radius, Color.green);
    }
}
