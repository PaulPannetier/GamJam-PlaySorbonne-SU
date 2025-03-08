 using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    private float lastTimeLaunch = float.MinValue;

    [SerializeField] private float cooldown;
    [SerializeField] protected float damage;

    [HideInInspector] public PlayerFightController fightController;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }


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

    //For putting the object in the hand of the player
    public virtual void SetPosition(Vector2 position, bool reversed)
    {

    }

    protected virtual void OnTouchEnemy(EnemyController enemy)
    {
        enemy.TakeDamage(damage);
    }

    protected virtual void Launch()
    {
        lastTimeLaunch = Time.time;
    }
}
