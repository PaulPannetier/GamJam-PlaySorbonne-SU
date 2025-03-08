using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Slash : Spell
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float instantiateRange = 2f;
    [SerializeField] private float timeBeforeDamage = 0.3f;

    [SerializeField] private float slashLifeTime = 0.6f;
    [SerializeField] private float damageRange;
    [SerializeField] private float damage;

    private GameObject slashObject;
    private Transform target;
    private float dmgTimer;

    SpriteRenderer spriteRenderer;

    public override void Cast(EnemyController enemy)
    {
        target = GetTarget(enemy);
        if (target == null) return;

        Vector2 dir = (target.position - enemy.transform.position).normalized;
        Vector2 prefabPosition = (Vector2)enemy.transform.position + dir * instantiateRange;

        slashObject = Instantiate(slashPrefab, prefabPosition, Quaternion.identity, enemy.transform);
        enemy.Callback(DealDamage, dmgTimer);

        spriteRenderer = slashObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = dir.x < 0;
        }

        Destroy(slashObject, slashLifeTime);

    }

    public override bool Condition(EnemyController enemy)
    {
        return true;
    }

    Transform GetTarget(EnemyController enemy)
    {
        return enemy.attackState.target;
    }

    void DealDamage()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(slashObject.transform.position, damageRange, (target.position - slashObject.transform.position).normalized);
        List<IDamageable> damageables = new List<IDamageable>();
        foreach (RaycastHit2D hit in hits)
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageables.Add(damageable);
            }
        }
        if (damageables.Count > 0)
        {
            foreach (IDamageable damageable in damageables)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
