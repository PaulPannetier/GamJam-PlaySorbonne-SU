using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;

public class Bullet : MonoBehaviour
{
    private Vector2 dir;
    private float damage;

    [SerializeField] private Vector2 collisionOffset;
    [SerializeField] private float collisionRadius;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask enemyMask; 

    public void Launch(in Vector2 dir, float damage)
    {
        this.dir = dir;
        this.damage = damage;
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position + collisionOffset, collisionRadius, enemyMask);
        foreach (Collider2D col in cols)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(damage);
            }
        }

        transform.position += (Vector3)dir * (speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Circle.GizmosDraw((Vector2)transform.position + collisionOffset, collisionRadius, Color.red);
    }

    private void OnValidate()
    {
        collisionRadius = Mathf.Max(0f, collisionRadius);
        speed = Mathf.Max(0f, speed);
    }
}
