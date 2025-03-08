using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float smoothSpeed = 5f;

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Aucune cible assignée à la caméra !");
            return;
        }

        Vector3 desiredPosition = target.position + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}