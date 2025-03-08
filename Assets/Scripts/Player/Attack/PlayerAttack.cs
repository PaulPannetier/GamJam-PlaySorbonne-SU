 using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    private float lastTimeLaunch = float.MinValue;

    [SerializeField] private float cooldown;
    [SerializeField] protected float damage;

    protected virtual void Update()
    {

    }

    public bool TryLaunch()
    {
        if(Time.time - lastTimeLaunch >= cooldown)
        {
            Launch();
            return true;
        }
        return false;
    }

    public virtual void SetPosition(Vector2 position)
    {

    }

    protected void OnTouchEnemy(EnemyController enemy)
    {
        print($"Touch enemy:{enemy.gameObject.name}");
    }

    protected virtual void Launch()
    {
        lastTimeLaunch = Time.time;
    }
}
