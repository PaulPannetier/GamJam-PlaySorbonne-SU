using UnityEngine;
using Collision2D;

public class GunAttack : PlayerAttack
{
    [SerializeField] private Bullet bulletPrefabs;
    [SerializeField] private Vector2 barrelOffset;

    [SerializeField] private bool drawGizmos;

    public override void SetPosition(Vector2 position)
    {
        base.SetPosition(position);
        transform.position = position;
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
