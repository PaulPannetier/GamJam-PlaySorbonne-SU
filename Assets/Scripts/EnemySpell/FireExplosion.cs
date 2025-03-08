using System.Collections.Generic;
using UnityEngine;

public class FireExplosion : Spell
{
    [SerializeField] private GameObject explosionPrefab; 
    [SerializeField] private float timeBeforeExplosion= 0.2f; 
    [SerializeField] private float timeBeforeDestroy = 2f;
    [SerializeField] private float timeBeforeDamage = 0.2f;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float explosionRange = 3f;

    private Transform target;
    public override void Cast(EnemyController enemy)
    {
        UpdateTarget(enemy);
        enemy.Callback(InstantiatePrefab, timeBeforeExplosion);
        enemy.Callback(DealDamage, timeBeforeDamage);

    }

    public override bool Condition(EnemyController enemy)
    {
        return true;
    }
    
    void UpdateTarget(EnemyController enemy)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(enemy.transform.position, attackRange, Vector2.zero);

        List<Transform> playersInRange = new List<Transform>();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                playersInRange.Add(hit.transform);
            }
        }

        target = GetClosestPlayer(enemy.transform.position, playersInRange);
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

    void InstantiatePrefab()
    {
        GameObject tempGO;
        tempGO = Instantiate(explosionPrefab, target.position, target.rotation);
        Destroy(tempGO, timeBeforeDestroy);
    }
    void DealDamage()
    {

    }
}
