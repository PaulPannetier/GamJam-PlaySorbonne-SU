using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Animator animator;

    [SerializeField] private float lootForce = 5f;
    [SerializeField] private GameObject[] loots;

    private bool isOpen = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public bool Activate()
    {
        if (isOpen)
            return false;

        isOpen = true;
        animator.SetBool("isOpen", true);
        StartCoroutine(OpenChest());
        return true;
    }


    private IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(1f);
        LootItem();
    }

    private void LootItem()
    {
        Destroy(GetComponent<Collider2D>());
        foreach (GameObject loot in loots)
        {
            GameObject newObject = Instantiate(loot, transform.position,transform.rotation,transform);
            Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                Vector2 force = Random.PointInCircle(transform.position,1f);
                
                rb.AddForce(force.normalized * lootForce ,ForceMode2D.Impulse);
            }
        }
    }
}
