using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = destination.position;
        }
    }
}
