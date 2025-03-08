using UnityEngine;
using System.Collections.Generic;

public class GoodMerchant : MonoBehaviour
{
    [SerializeField] private List<Item> itemsPrefabs;
    [SerializeField] private List<SellAttack> attacksPrefabs;
    [SerializeField] private List<Transform> itemsPosition;
    [SerializeField] private ShopItem shopItemPrefabs;

    private void Start()
    {
        int end = Mathf.Min(itemsPrefabs.Count + attacksPrefabs.Count, itemsPosition.Count);
        for (int i = 0; i < end; i++)
        {
            ShopItem shopItem;
            if(i < itemsPrefabs.Count)
            {
                shopItem = Instantiate(shopItemPrefabs, itemsPosition[i].position, Quaternion.identity, itemsPosition[i]);
                shopItem.Init(this, itemsPrefabs[i]);
            }
            else
            {
                int j = i - itemsPrefabs.Count;
                shopItem = Instantiate(shopItemPrefabs, itemsPosition[i].position, Quaternion.identity, itemsPosition[j]);
                SellAttack sellAttack = attacksPrefabs[j];
                shopItem.Init(this, sellAttack.playerAttackPrefabs, sellAttack.cost, sellAttack.icon);
            }
        }
    }

    public bool TryBuy(Item item, PlayerInventory inventory)
    {
        return inventory.TryBuy(item);
    }

    public bool TryBuy(PlayerAttack attack, PlayerInventory inventory)
    {
        int cost = attacksPrefabs.Find((SellAttack sa) => sa.playerAttackPrefabs == attack).cost;
        return inventory.TryBuy(attack, cost);
    }

    [System.Serializable]
    private struct SellAttack
    {
        public PlayerAttack playerAttackPrefabs;
        public Sprite icon;
        public int cost;
    }
}
