using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolState : IEnemyState
{
    [SerializeField] private float ChasePlayerRange = 10f;

    
    private Transform target;

    private Coroutine updateTargetCoroutine;
    public void EnterState(EnemyController enemy)
    {
        
        updateTargetCoroutine = enemy.StartCoroutine(UpdateTargetRoutine(enemy));
    }

    public void UpdateState(EnemyController enemy)
    {

    }

    public void ExitState(EnemyController enemy)
    {

    }

    #region UpdateTarget
    private IEnumerator UpdateTargetRoutine(EnemyController enemy)
    {
        while (true)
        {
            target = UpdateTarget(enemy);
            yield return new WaitForSeconds(0.5f);
        }
    }

    Transform UpdateTarget(EnemyController enemy)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(enemy.transform.position, ChasePlayerRange, Vector2.zero);

        List<Transform> playersInRange = new List<Transform>();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                playersInRange.Add(hit.transform);
            }
        }

        return GetClosestPlayer(enemy.transform.position, playersInRange);
    }

    
    public Transform GetClosestPlayer(Vector2 currentPosition, List<Transform> playersInRange)
    {
        if (playersInRange == null || playersInRange.Count == 0)
            return null; // Aucun joueur dans la liste

        Transform closestPlayer = null;
        float closestDistanceSqr = float.MaxValue; // Distance la plus courte trouvée

        foreach (Transform player in playersInRange)
        {
            float distanceSqr = (player.position - (Vector3)currentPosition).sqrMagnitude; // Distance au carré
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }

    #endregion
}