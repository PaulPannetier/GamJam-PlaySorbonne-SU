using UnityEngine;
using Collision2D;

public class GunAttack : PlayerAttack
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Bullet bulletPrefabs;
    [SerializeField] private Vector2 barrelOffset;

    [SerializeField] private bool drawGizmos;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void SetPosition(Vector2 position, bool reversed)
    {
        base.SetPosition(position, reversed);
        transform.position = position;
        spriteRenderer.flipX = reversed;
    }

    protected override void Launch()
    {
        base.Launch();

        Bullet bullet = Instantiate(bulletPrefabs, (Vector2)transform.position + barrelOffset, Quaternion.identity);
        Vector2 dir = ((Vector2)Camera.main.ScreenToWorldPoint(InputManager.mousePosition) - (Vector2)transform.position).normalized;
        bullet.Launch(dir, this);
    }

    public void OnBulletTouch(Bullet bullet, EnemyController enemyController)
    {
        base.OnTouchEnemy(enemyController);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDrawGizmosSelected()
    {
        if(!drawGizmos)
            return;

        Circle.GizmosDraw((Vector2)transform.position + barrelOffset, 0.1f, Color.green);
    }
}
