using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Walk")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed;
    [SerializeField, Range(0f, 1f)] private float initSpeed;
    [SerializeField] private float speedLerp;

    [Header("Collision")]
    [SerializeField] private Vector2 hitboxOffset;
    [SerializeField] private Vector2 hitboxSize;
    [SerializeField] private float verticalRayOffset;

    private void Awake()
    {
        
    }

    private void LateUpdate()
    {
        
    }

    private void OnValidate()
    {
        verticalRayOffset = Mathf.Max(0f, verticalRayOffset);
    }
}
