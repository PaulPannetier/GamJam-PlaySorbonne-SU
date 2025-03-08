using UnityEngine;
using Collision2D;

public class GeyserAttack : PlayerAttack
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int nbGeyser;
    [SerializeField] private float radius;
    [SerializeField] private Geyser geyserPrefabs;

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

        float angle = 0f;
        Vector2 center = fightController.transform.position;
        for (int i = 0; i < nbGeyser; i++)
        {
            Vector2 offset = Useful.Vector2FromAngle(angle, radius);
            Geyser geyser = Instantiate(geyserPrefabs, center + offset, Quaternion.identity);
            geyser.Launch(this);
            angle += 2f * Mathf.PI / nbGeyser;
        }
    }

    public void OnGeyserTouch(Geyser geyser, EnemyController enemy)
    {
        base.OnTouchEnemy(enemy);
    }

    private void OnValidate()
    {
        nbGeyser = Mathf.Max(nbGeyser, 0);
        radius = Mathf.Max(radius, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Circle.GizmosDraw(transform.position, radius, Color.green);
    }
}
