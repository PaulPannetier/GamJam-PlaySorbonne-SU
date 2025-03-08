using UnityEngine;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;

public class ItemHandler : MonoBehaviour, ICollectable
{
    private bool isCollected = false;
    private Transform target;
    private Collider2D col;
    private Rigidbody2D rb;

    [SerializeField] private Item item;
    [SerializeField] private float collectSpeed = 2f;
    [SerializeField] private float collectDistance = 0.4f;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Collect(Transform target)
    {
        isCollected = true;
        col.enabled = false;
        this.target = target;
        target.GetComponent<PlayerInventory>().AddItem(item);
    }

    public bool CanBeCollect(Transform target)
    {
        return !isCollected && transform.position.SqrDistance(transform.position) < collectDistance * collectDistance;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            rb.linearVelocity = (target.position - transform.position).normalized * collectSpeed;
            if (Vector2.Distance(target.position, transform.position) < collectDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Circle.GizmosDraw(transform.position, collectDistance, Color.yellow);
    }
}
