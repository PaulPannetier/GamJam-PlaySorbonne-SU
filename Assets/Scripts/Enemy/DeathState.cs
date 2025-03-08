using UnityEngine;

[System.Serializable]
public class DeathState : IEnemyState
{
    [SerializeField] private float timeBeforeDestroy = 1.5f;
    private bool isDead = false;

    public void EnterState(EnemyController enemy)
    {
        isDead = true;
        enemy.animator.SetBool("isDead", isDead);
        GameObject.Destroy(enemy.gameObject, timeBeforeDestroy);
    }

    public void UpdateState(EnemyController enemy)
    {
        
    }

    public void ExitState(EnemyController enemy)
    {
        isDead = false;
        enemy.animator.SetBool("isDead", isDead);
    }
}
