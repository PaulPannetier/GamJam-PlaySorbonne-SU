using UnityEngine;
using Collision2D;

public class ElectrickPulseAttack : PowerUpAttack
{
    [SerializeField] private ElectrickPulse electrickPulsePrefab;
    [SerializeField] private int nbElectrickPulse;
    [SerializeField] private float radius;
    [SerializeField] private float angularSpeed;
    [SerializeField] private float duration;
    [SerializeField, Range(0f, 360f)] private float startAngle;

    [SerializeField] private bool drawGizmos;

    public void OnElectrickPulseTouchEnemy(ElectrickPulse electrickPulse, IDamageable enemy)
    {
        base.OnTouchEnemy(enemy);
    }

    public override void SetPosition(Vector2 position, bool reversed)
    {

    }

    protected override void Launch()
    {
        base.Launch();

        float angle = this.startAngle;
        float step = 360f / nbElectrickPulse;
        int nbElecPulse = nbElectrickPulse + GetPowerUp();
        for (int i = 0; i < nbElecPulse; i++)
        {
            Vector2 offset = Useful.Vector2FromAngle(angle * Mathf.Deg2Rad, radius);
            ElectrickPulse electrickPulse = Instantiate(electrickPulsePrefab, (Vector2)transform.position + offset, Quaternion.identity);
            electrickPulse.Launch(this, fightController, radius, angularSpeed, duration);
            angle = (angle + step) % 360f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
            return;

        Circle.GizmosDraw((Vector2)transform.position, radius, Color.red);
    }

    private void OnValidate()
    {
        radius = Mathf.Max(0f, radius);
        angularSpeed = Mathf.Max(0f, angularSpeed);
    }
}
