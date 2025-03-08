using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class HurtState : IEnemyState
{
    [SerializeField] private float hurtDuration = 0.5f;
    [SerializeField] private bool isHurt = false;

    private float timer;
    public void EnterState(EnemyController enemy)
    {
        timer = hurtDuration;
        isHurt = true;
        enemy.animator.SetBool("isHurt", isHurt);
    }
    
    public void UpdateState(EnemyController enemy)
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            enemy.BasicTransitionToState(enemy.lastState);
        }
    }

    public void ExitState(EnemyController enemy)
    {
        isHurt = false;
        enemy.animator.SetBool("isHurt", isHurt);
    }

}
