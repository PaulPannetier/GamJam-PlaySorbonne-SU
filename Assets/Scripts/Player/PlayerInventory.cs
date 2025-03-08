using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> items;

    [SerializeField] private LayerMask collectableMask;
    [SerializeField] private float collectableRadius;

    public int nbCoin {  get; private set; }

    private void Awake()
    {
        nbCoin = 0;
        items = new List<Item>();
    }

    public void CollectCoin(Coin coin)
    {
        nbCoin++;
    }

    public void EarnCoins(int nbCoins)
    {
        nbCoin += nbCoins;
    }

    public float GetBonusDamagePercent(PlayerAttack attack, IDamageable enemy)
    {
        float damagePercent = 0f;
        foreach (Item item in items)
        {
            damagePercent += item.GetBonusDamagePercent(attack, enemy);
        }
        return damagePercent;
    }

    public float GetBonusDamage(PlayerAttack attack, IDamageable enemy)
    {
        float damagePercent = 0f;
        foreach (Item item in items)
        {
            damagePercent += item.GetBonusDamage(attack, enemy);
        }
        return damagePercent;
    }

    public int GetPowerUp(PlayerAttack attack)
    {
        int powerUp = 0;
        foreach (Item item in items)
        {
            powerUp += item.GetPowerUp(attack);
        }
        return powerUp;
    }

    private void Update()
    {
        HandleCollectable();
    }

    private void HandleCollectable()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, collectableRadius, collectableMask);
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                if (collectable.CanBeCollect(transform))
                {
                    collectable.Collect(transform);
                }
            }
        }
    }

    private void OnValidate()
    {
        collectableRadius = Mathf.Max(collectableRadius, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Circle.GizmosDraw(transform.position, collectableRadius);
    }
}
