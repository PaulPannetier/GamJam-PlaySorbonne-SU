using UnityEngine;
using Collision2D;
using Mono.Cecil;

public class CharController : MonoBehaviour
{
    private new Transform transform;
    private RaycastHit2D topRay, topLeftRay, topRightRay;
    private RaycastHit2D downRay, downLeftRay, downRightRay;
    private RaycastHit2D rightRay, rightUpRay, rightDownRay;
    private RaycastHit2D leftRay, leftUpRay, leftDownRay;
    private Inputs playerInput;

    private Vector2 velocity;

    [SerializeField] private bool drawGizmos;

    [Header("Walk")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed;
    [SerializeField, Range(0f, 1f)] private float initSpeed;
    [SerializeField] private float speedLerp;

    [Header("Collision")]
    [SerializeField] private Vector2 hitboxOffset;
    [SerializeField] private Vector2 hitboxSize;
    [SerializeField] private float verticalRayOffset;
    [SerializeField] private float verticalRayLength;
    [SerializeField] private float horizontalRayOffset;
    [SerializeField] private float horizontalRayLength;

    [Header("Inputs")]
    [SerializeField] private InputKey up;
    [SerializeField] private InputKey down;
    [SerializeField] private InputKey right;
    [SerializeField] private InputKey left;

    private void Awake()
    {
        this.transform = base.transform;
    }

    private void CreateInputs()
    {
        if(InputManager.IsGamePadConnected(ControllerType.Gamepad1))
        {
            Vector2 stick = InputManager.GetGamepadStickPosition(ControllerType.Gamepad1, GamepadStick.left);
            int rawX = (int)stick.x.Sign();
            int rawY = (int)stick.y.Sign();
            playerInput = new Inputs(stick.x, stick.y, rawX, rawY);
            return;
        }

        float y = InputManager.GetKeyDown(up) ? 1f : (InputManager.GetKeyDown(down) ? -1f : 0f);
    }

    private void LateUpdate()
    {
        CreateInputs();

        Vector2 center = (Vector2)transform.position + hitboxOffset;
        rightRay = Physics2D.Raycast(center + Vector2.right * hitboxSize.x * 0.4f, Vector2.right, horizontalRayLength, groundMask);
        rightUpRay = Physics2D.Raycast(center + new Vector2(hitboxSize.x * 0.4f, horizontalRayOffset), Vector2.right, horizontalRayLength, groundMask);
        rightDownRay = Physics2D.Raycast(center + new Vector2(hitboxSize.x * 0.4f, -horizontalRayOffset), Vector2.right, horizontalRayLength, groundMask);

        leftRay = Physics2D.Raycast(center + new Vector2(-hitboxSize.x * 0.4f, 0f), Vector2.left, horizontalRayLength, groundMask);
        leftUpRay = Physics2D.Raycast(center + new Vector2(-hitboxSize.x * 0.4f, horizontalRayOffset), Vector2.left, horizontalRayLength, groundMask);
        leftDownRay = Physics2D.Raycast(center + new Vector2(-hitboxSize.x * 0.4f, -horizontalRayOffset), Vector2.left, horizontalRayLength, groundMask);

        topRay = Physics2D.Raycast(center + new Vector2(0f, hitboxSize.y * 0.4f), Vector2.up, verticalRayLength, groundMask);
        topRightRay = Physics2D.Raycast(center + new Vector2(verticalRayOffset, hitboxSize.y * 0.4f), Vector2.up, verticalRayLength, groundMask);
        topLeftRay = Physics2D.Raycast(center + new Vector2(-verticalRayOffset, hitboxSize.y * 0.4f), Vector2.up, verticalRayLength, groundMask);

        downRay = Physics2D.Raycast(center + new Vector2(0f, -hitboxSize.y * 0.4f), Vector2.down, verticalRayLength, groundMask);
        downRightRay = Physics2D.Raycast(center + new Vector2(verticalRayOffset, -hitboxSize.y * 0.4f), Vector2.down, verticalRayLength, groundMask);
        downLeftRay = Physics2D.Raycast(center + new Vector2(-verticalRayOffset, -hitboxSize.y * 0.4f), Vector2.down, verticalRayLength, groundMask);





        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void OnValidate()
    {
        this.transform = base.transform;
        verticalRayOffset = Mathf.Max(0f, verticalRayOffset);
        verticalRayLength = Mathf.Max(0f, verticalRayLength);
        horizontalRayOffset = Mathf.Max(0f, horizontalRayOffset);
        horizontalRayLength = Mathf.Max(0f, horizontalRayLength);
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
            return;

        Hitbox.GizmosDraw((Vector2)transform.position + hitboxOffset, hitboxSize, Color.green);
        Gizmos.color = Color.red;

        Vector2 center = hitboxOffset + (Vector2)transform.position;
        
        //right
        Gizmos.DrawLine(center + Vector2.right * hitboxSize.x * 0.4f, center + Vector2.right * (hitboxSize.x * 0.4f + horizontalRayLength));
        Gizmos.DrawLine(center + new Vector2(hitboxSize.x * 0.4f, horizontalRayOffset), center + new Vector2(hitboxSize.x * 0.4f + horizontalRayLength, horizontalRayOffset));
        Gizmos.DrawLine(center + new Vector2(hitboxSize.x * 0.4f, -horizontalRayOffset), center + new Vector2(hitboxSize.x * 0.4f + horizontalRayLength, -horizontalRayOffset));

        //left
        Gizmos.DrawLine(center + Vector2.left * hitboxSize.x * 0.4f, center + Vector2.left * (hitboxSize.x * 0.4f + horizontalRayLength));
        Gizmos.DrawLine(center + new Vector2(-hitboxSize.x * 0.4f, horizontalRayOffset), center + new Vector2(-hitboxSize.x * 0.4f - horizontalRayLength, horizontalRayOffset));
        Gizmos.DrawLine(center + new Vector2(-hitboxSize.x * 0.4f, -horizontalRayOffset), center + new Vector2(-hitboxSize.x * 0.4f - horizontalRayLength, -horizontalRayOffset));

        //up
        Gizmos.DrawLine(center + Vector2.up * hitboxSize.y * 0.4f, center + Vector2.up * (hitboxSize.y * 0.4f + verticalRayLength));
        Gizmos.DrawLine(center + new Vector2(verticalRayOffset, hitboxSize.y * 0.4f), center + new Vector2(verticalRayOffset, hitboxSize.y * 0.4f + verticalRayLength));
        Gizmos.DrawLine(center + new Vector2(-verticalRayOffset, hitboxSize.y * 0.4f), center + new Vector2(-verticalRayOffset, hitboxSize.y * 0.4f + verticalRayLength));

        //down
        Gizmos.DrawLine(center + Vector2.down * hitboxSize.y * 0.4f, center + Vector2.down * (hitboxSize.y * 0.4f + verticalRayLength));
        Gizmos.DrawLine(center + new Vector2(verticalRayOffset, -hitboxSize.y * 0.4f), center + new Vector2(verticalRayOffset, -hitboxSize.y * 0.4f - verticalRayLength));
        Gizmos.DrawLine(center + new Vector2(-verticalRayOffset, -hitboxSize.y * 0.4f), center + new Vector2(-verticalRayOffset, -hitboxSize.y * 0.4f - verticalRayLength));
    }

    private struct Inputs
    {
        public float x, y;
        public int rawX, rawY;

        public Inputs(float x, float y, int rawX, int rawY)
        {
            this.x = x;
            this.y = y;
            this.rawX = rawX;
            this.rawY = rawY;
        }
    }
}
