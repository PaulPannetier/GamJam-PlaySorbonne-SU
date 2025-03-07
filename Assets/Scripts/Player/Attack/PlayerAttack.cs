 using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float lastTimeLaunch = -10f;

    [SerializeField] private InputKey activate;
    [SerializeField] private float cooldown;

    private void Update()
    {
        if(InputManager.GetKeyDown(activate))
        {
            TryLaunch();
        }
    }

    private void TryLaunch()
    {
        if(Time.time - lastTimeLaunch >= cooldown)
        {
            Launch();
        }
    }

    protected void OnTouchEnemy(EnemyController enemy)
    {

    }

    protected virtual void Launch()
    {
        lastTimeLaunch = Time.time;
    }
}
