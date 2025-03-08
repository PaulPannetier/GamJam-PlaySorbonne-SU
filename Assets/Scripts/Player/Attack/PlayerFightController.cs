using UnityEngine;
using Collision2D;

public class PlayerFightController : MonoBehaviour
{
    private bool flipX, oldFlipX;
    private CharController characterController;

    [SerializeField] private InputManager.GeneralInput inputAttack1;
    [SerializeField] private InputManager.GeneralInput inputAttack2;
    [SerializeField] private Vector2 attack1Offset, attack2Offset;

    [SerializeField] private PlayerAttack _attack1, _attack2;
    public PlayerAttack attack1
    {
        get => _attack1;
        set
        {
            _attack1 = value;
            if (_attack1 != null)
                _attack1.fightController = this;
            ResetAttack1Position();
        }
    }
    public PlayerAttack attack2
    {
        get => _attack2;
        set
        {
            _attack2 = value;
            if(_attack2 != null)
                _attack2.fightController = this;
            ResetAttack2Position();
        }
    }

    public bool drawGizmos;

    private void Awake()
    {
        characterController = GetComponent<CharController>();
    }

    private void Start()
    {
        attack1 = attack1;
        attack2 = attack2;
        ResetAttack1Position();
        ResetAttack2Position();
    }

    private void ResetAttack1Position()
    {
        if (attack1 != null)
        {
            attack1.SetPosition((Vector2)transform.position + attack1Offset, flipX);
        }
    }

    private void ResetAttack2Position()
    {
        if (attack2 != null)
        {
            attack2.SetPosition((Vector2)transform.position + attack2Offset, flipX);
        }
    }

    private void Update()
    {
        if(inputAttack1.IsPressedDown())
        {
            attack1?.TryLaunch();
        }

        if (inputAttack2.IsPressedDown())
        {
            attack2?.TryLaunch();
        }

        flipX = characterController.flipX;

        if (flipX != oldFlipX)
        {
            Vector2 tmp = attack1Offset;
            attack1Offset = attack2Offset;
            attack2Offset = tmp;

            ResetAttack1Position();
            ResetAttack2Position();
        }
        oldFlipX = flipX;
    }

    private void OnDrawGizmosSelected()
    {
        if(!drawGizmos)
            return;

        Circle.GizmosDraw((Vector2)transform.position + attack1Offset, 0.2f, Color.blue);
        Circle.GizmosDraw((Vector2)transform.position + attack2Offset, 0.2f, Color.green);
    }
}
