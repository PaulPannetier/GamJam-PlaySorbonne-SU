using UnityEngine;
using Collision2D;

public class GunAttack : PlayerAttack
{
    private SpriteRenderer spriteRenderer;
    private CharController characterController;

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

    private Vector2 GetCurrentDirection()
    {
        if (InputManager.IsGamePadConnected(ControllerType.Gamepad1))
        {
            Vector2 dir = characterController.GetCurrentDirection();
            if (dir.sqrMagnitude < 1e-5f)
                return Vector2.right;
            return dir.normalized;
        }
        return ((Vector2)Camera.main.ScreenToWorldPoint(InputManager.mousePosition) - (Vector2)transform.position).normalized;
    }

    protected override void Launch()
    {
        base.Launch();
        characterController = fightController.GetComponent<CharController>();
        Bullet bullet = Instantiate(bulletPrefabs, (Vector2)transform.position + barrelOffset, Quaternion.identity);
        Vector2 dir = GetCurrentDirection();
        bullet.Launch(dir, this);
    }

    public void OnBulletTouch(Bullet bullet, IDamageable enemy)
    {
        base.OnTouchEnemy(enemy);
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
