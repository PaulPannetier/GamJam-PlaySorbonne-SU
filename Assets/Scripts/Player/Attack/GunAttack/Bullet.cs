using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    private Vector2 dir;
    private GunAttack gunAttack;
    private List<IDamageable> enemyAlreadyTouch;

    [SerializeField] private Vector2 collisionOffset;
    [SerializeField] private float collisionRadius;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float maxLifeDuration = 10f;

    [SerializeField] private bool drawGizmos;

    public void Launch(in Vector2 dir, GunAttack gunAttack)
    {
        this.dir = dir;
        transform.rotation = Quaternion.Euler(0f, 0f, Useful.AngleHori(Vector2.zero, dir) * Mathf.Rad2Deg);
        this.gunAttack = gunAttack;
        enemyAlreadyTouch = new List<IDamageable>();
        this.Invoke(() => Destroy(gameObject), maxLifeDuration);
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position + collisionOffset, collisionRadius, enemyMask);
        foreach (Collider2D col in cols)
        {
            IDamageable enemy = col.GetComponent<IDamageable>();
            if (enemy != null && !enemyAlreadyTouch.Contains(enemy))
            {
                gunAttack.OnBulletTouch(this, enemy);
                enemyAlreadyTouch.Add(enemy);
            }
        }

        transform.position += (Vector3)dir * (speed * Time.deltaTime);
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
        speed = Mathf.Max(0f, speed);
    }
}
