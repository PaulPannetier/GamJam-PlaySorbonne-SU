using UnityEngine;
using System.Collections.Generic;

public class PlayerCollection : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private float collectRadius = 2f;

    [SerializeField] private bool drawGizmos;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryActivateInteractable();
        }
        Collect();
    }

    private void TryActivateInteractable()
    {
        Transform[] interactables = GetInteractableInRange(interactRange);
        if (interactables.Length > 0)
        {
            Transform closest = GetCloser(interactables);
            if (closest.TryGetComponent(out IInteractable interactable))
            {
                interactable.Activate();
            }
        }
    }

    private void Collect()
    {
        foreach (Transform t in GetInteractableInRange(collectRadius))
        {
            ICollectable collectable = t.GetComponent<ICollectable>();
            if (collectable != null)
            {
                collectable.Collect(transform);
            }

        }
    }

    private Transform[] GetInteractableInRange(float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, interactableMask);
        List<Transform> interactableTransforms = new List<Transform>();

        foreach (Collider2D collider in colliders)
        {
            interactableTransforms.Add(collider.transform);
        }

        return interactableTransforms.ToArray();
    }

    private Transform GetCloser(Transform[] transforms)
    {
        Transform closest = null;
        float minDistance = float.MaxValue;
        Vector2 playerPosition = transform.position;

        foreach (Transform t in transforms)
        {
            float distance = Vector2.Distance(playerPosition, t.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = t;
            }
        }

        return closest;
    }

    public void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectRadius);
    }
}
