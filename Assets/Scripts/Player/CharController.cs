using UnityEngine;

public class CharController : MonoBehaviour
{
    private Inputs playerInput;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 velocity;

    [SerializeField] private bool drawGizmos;

    [Header("Walk")]
    [SerializeField] private float walkSpeed;
    [SerializeField, Range(0f, 1f)] private float initSpeed;
    [SerializeField] private float speedLerp;

    [Header("Inputs")]
    [SerializeField] private InputKey up;
    [SerializeField] private InputKey down;
    [SerializeField] private InputKey right;
    [SerializeField] private InputKey left;

    public bool flipX {  get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector2 GetCurrentDirection() => new Vector2(playerInput.x, playerInput.y);
    public Vector2 GetCurrentSpeed() => velocity;

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

        bool r = InputManager.GetKey(right);
        bool l = InputManager.GetKey(left);
        bool u = InputManager.GetKey(up);
        bool d = InputManager.GetKey(down);
        float x = r && l ? 0f : (r || l ? (r ? 1f : -1f) : 0f);
        float y = u && d ? 0f : (u || d ? (u ? 1f : -1f) : 0f);

        playerInput = new Inputs(x, y, (int)x.Sign(), (int)y.Sign());
    }

    private void Update()
    {
        CreateInputs();

        spriteRenderer.flipX = flipX;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(playerInput.rawX, playerInput.rawY);
        targetVelocity.Normalize();
        targetVelocity.x *= Mathf.Abs(playerInput.x);
        targetVelocity.y *= Mathf.Abs(playerInput.y);
        targetVelocity *= walkSpeed;

        if (playerInput.rawX != 0 || playerInput.rawY != 0)
        {
            if ((playerInput.x <= 0f && velocity.x >= 0f) || (playerInput.x >= 0f && velocity.x <= 0f))
            {
                velocity.x = Mathf.MoveTowards(velocity.x, 0f, 2f * speedLerp * Time.deltaTime);
            }
            if ((playerInput.y <= 0f && velocity.y >= 0f) || (playerInput.y >= 0f && velocity.y <= 0f))
            {
                velocity.y = Mathf.MoveTowards(velocity.y, 0f, 2f * speedLerp * Time.deltaTime);
            }

            if (playerInput.rawX != 0 && Mathf.Abs(velocity.x) < Mathf.Abs(targetVelocity.x) * initSpeed)
            {
                velocity.x = targetVelocity.x * initSpeed;
            }
            if (playerInput.rawY != 0 && Mathf.Abs(velocity.y) < Mathf.Abs(targetVelocity.y) * initSpeed)
            {
                velocity.y = targetVelocity.y * initSpeed;
            }

            velocity = Vector2.MoveTowards(velocity, targetVelocity, speedLerp * Time.deltaTime);
        }
        else
        {
            velocity = Vector2.zero;
        }

        rb.linearVelocity = velocity;

        if(Mathf.Abs(velocity.x) > 1e-4f)
        {
            flipX = velocity.x < 0f;
        }
    }

    private void OnValidate()
    {
        walkSpeed = Mathf.Max(0f, walkSpeed);
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
