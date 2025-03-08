using UnityEngine;
using Collision2D;

public class GunAttack : PlayerAttack
{
    [SerializeField] private Bullet buletPrefabs;
    [SerializeField] private Vector2 barrelOffset;

    public override void SetPosition(Vector2 position)
    {
        base.SetPosition(position);
        transform.position = position;
    }

    protected override void Launch()
    {
        base.Launch();

        Bullet bullet = Instantiate(buletPrefabs, (Vector2)transform.position + barrelOffset, Quaternion.identity, transform);
        Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(InputManager.mousePosition) - (Vector2)transform.position;
        bullet.Launch(dir, damage);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDrawGizmosSelected()
    {
        Circle.GizmosDraw((Vector2)transform.position + barrelOffset, 0.2f, Color.green);
    }
}
