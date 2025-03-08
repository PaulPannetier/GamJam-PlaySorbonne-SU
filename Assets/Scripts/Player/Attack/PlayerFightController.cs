using UnityEngine;
using Collision2D;

public class PlayerFightController : MonoBehaviour
{
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
            ResetAttack1Position();
        }
    }
    public PlayerAttack attack2
    {
        get => _attack2;
        set
        {
            _attack2 = value;
            ResetAttack2Position();
        }
    }

    public bool drawGizmos;

    private void Start()
    {
        ResetAttack1Position();
        ResetAttack2Position();
    }

    private void ResetAttack1Position()
    {
        if (attack1 != null)
        {
            attack1.SetPosition((Vector2)transform.position + attack1Offset);
        }
    }

    private void ResetAttack2Position()
    {
        if (attack2 != null)
        {
            attack2.SetPosition((Vector2)transform.position + attack2Offset);
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
    }

    private void OnDrawGizmosSelected()
    {
        if(!drawGizmos)
            return;

        Circle.GizmosDraw((Vector2)transform.position + attack1Offset, 0.2f, Color.blue);
        Circle.GizmosDraw((Vector2)transform.position + attack2Offset, 0.2f, Color.green);
    }
}
