using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion : Spell
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float timeBeforeExplosion = 0.2f;
    [SerializeField] private float timeBeforeDestroy = 2f;
    [SerializeField] private float timeBeforeDamage = 0.2f;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float explosionRange = 3f;
    [SerializeField] private int damage = 1;

    private Transform target;

    void Start()
    {
        Cast();
    }

    private void Cast()
    {

        UpdateTarget();
        InstantiatePrefab();
        DealDamage();
    }

    public override void Cast(EnemyController enemy)
    {
        //plus utile mais interface 
    }

    public override bool Condition(EnemyController enemy)
    {
        return true;
    }

    void UpdateTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero);

        List<Transform> playersInRange = new List<Transform>();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                playersInRange.Add(hit.transform);
            }
        }

        target = GetClosestPlayer(transform.position, playersInRange);
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
        Debug.Log("lolilol");
        if (target != null)
        {
            GameObject tempGO;
            tempGO = Instantiate(explosionPrefab, target.position, target.rotation);
            Destroy(tempGO, timeBeforeDestroy);
        }
        Destroy(gameObject, timeBeforeDestroy);
    }

    #region old
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


    #endregion
    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(timeBeforeDamage);

        if (target == null)
        {
            yield break;
        }

        RaycastHit2D[] hits = Physics2D.CircleCastAll(target.position, explosionRange, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            HealthController healthController = hit.transform.GetComponent<HealthController>();
            {
                if (healthController != null)
                {
                    healthController.TakeDamage(damage);
                }
            }
        }
    }
}
