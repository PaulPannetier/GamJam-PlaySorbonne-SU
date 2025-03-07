using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private PlayerFightController fightController;
    [SerializeField] private List<Item> items;

    [SerializeField] private LayerMask collectableMask;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float collectableRadius;

    [SerializeField] private int nbCoins;

    private void Awake()
    {
        nbCoins = 0;
        items = new List<Item>();
        fightController = GetComponent<PlayerFightController>();
    }

    public void CollectCoin(Coin coin)
    {
        nbCoins++;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        item.OnAttach(GetComponent<PlayerFightController>());
    }

    public void EarnCoins(int nbCoins)
    {
        this.nbCoins += nbCoins;
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

    public bool TryBuy(Item item)
    {
        if(nbCoins >= item.cost)
        {
            AddItem(item);
            nbCoins -= item.cost;
            return true;
        }
        return false;
    }

    public bool TryBuy(PlayerAttack attack, int cost)
    {
        if (nbCoins >= cost)
        {
            int nbAttack = fightController.GetNbAttack();
            if(nbAttack >= 4)
                return false;

            fightController.SetAttack(nbAttack, attack);
            nbCoins -= cost;
            return true;
        }
        return false;
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

        if(InputManager.GetKeyDown(KeyCode.E))
        {
            cols = Physics2D.OverlapCircleAll(transform.position, collectableRadius, interactableMask);
            foreach (Collider2D col in cols)
            {
                if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.Activate();
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
