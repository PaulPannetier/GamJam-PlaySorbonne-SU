using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashLauncher : Spell
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float instantiateRange = 1f;
    [SerializeField] private float timeBeforeDamage = 0.3f;

    [SerializeField] private float slashLifeTime = 0.6f;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage = 1;

    private GameObject slashObject;
    private Transform target;
    private float dmgTimer;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        Cast();
    }

    private void Cast()
    {
        UpdateTarget();
        if (target == null) return;

        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 prefabPosition = (Vector2)transform.position + dir * instantiateRange;

        slashObject = Instantiate(slashPrefab, prefabPosition, Quaternion.identity, transform);

        spriteRenderer = slashObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = dir.x < 0;
        }

        Destroy(slashObject, slashLifeTime);
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

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(timeBeforeDamage);

        if (target == null)
        {
            yield break;
        }
        
        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 prefabPosition = (Vector2)transform.position + dir * instantiateRange;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(prefabPosition, attackRange, Vector2.zero);
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





    public override void Cast(EnemyController enemy)
    {

    }

    public override bool Condition(EnemyController enemy)
    {
        return true;
    }


}
