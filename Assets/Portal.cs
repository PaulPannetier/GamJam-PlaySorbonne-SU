using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = destination.position;
        }
    }
}
