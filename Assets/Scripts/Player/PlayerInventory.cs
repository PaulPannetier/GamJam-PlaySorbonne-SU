using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private LayerMask collectableMask;
    [SerializeField] private float collectableRadius;

    public int nbCoin {  get; private set; }

    private void Awake()
    {
        nbCoin = 0;
    }

    public void CollectCoin(Coin coin)
    {
        nbCoin++;
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
