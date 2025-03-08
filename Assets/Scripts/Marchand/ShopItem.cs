using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;

public class ShopItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Vector2 detectionHitbox;
    [SerializeField] private InputManager.GeneralInput buyInput;
    [SerializeField] private LayerMask playerMask;

    public int cost;
    public PlayerAttack attack;
    public Item item;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(GoodMerchant merchant, PlayerAttack attack, int cost, Sprite icon)
    {
        this.attack = attack;
        item = null;
        this.cost = cost;
        spriteRenderer.sprite = icon;
    }

    public void Init(GoodMerchant merchant, Item item)
    {
        this.item = item;
        this.cost = item.cost;
        attack = null;
        spriteRenderer.sprite = item.icon;
    }

    private void Update()
    {
        Collider2D col = Physics2D.OverlapBox(transform.position, detectionHitbox, playerMask);
        if(col != null)
        {
            PlayerInventory inventory = col.GetComponent<PlayerInventory>();
            if(inventory != null)
            {
                if(buyInput.IsPressedDown())
                {
                    if(item != null)
                    {
                        if (inventory.TryBuy(item))
                        {
                            Destroy(gameObject);
                        }
                    }
                    else
                    {
                        if (inventory.TryBuy(attack, cost))
                        {
                            Destroy(gameObject);
                        }
                    }

                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Hitbox.GizmosDraw(transform.position, detectionHitbox, Color.green);
    }
}
