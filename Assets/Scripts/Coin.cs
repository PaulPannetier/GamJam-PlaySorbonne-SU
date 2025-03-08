using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{   
    [SerializeField] private float collectSpeed = 2f;
    [SerializeField] private float collectDistance = 0.4f;
    private bool isCollected = false;
    private Transform target;

    Collider2D col;
    Rigidbody2D rb;

    
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Collect(Transform target)
    {
        if(isCollected)
        return;

        isCollected = true;
        col.enabled = false;
        this.target = target;
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            rb.linearVelocity = (target.position - transform.position).normalized * collectSpeed;
            if(Vector2.Distance(target.position, transform.position) < collectDistance )
            {
                Destroy(gameObject);
            }
        }
    }
}
