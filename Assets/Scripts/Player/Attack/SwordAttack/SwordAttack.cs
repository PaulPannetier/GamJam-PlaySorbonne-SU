using UnityEngine;
using System.Collections.Generic;
using Collision2D;
using Collider2D = UnityEngine.Collider2D;

public class SwordAttack : PlayerAttack
{
    private List<EnemyController> enemyAlreadyTouch;
    private PlayerAnimator playerAnimator;
    private Animator animator;
    private Vector2 currentOffset, currentSize;
    private bool isAttackEnable;
    private float attackDuration;
    private float lastTimeBeginAttack = float.MinValue;

    [SerializeField] private Vector2 offsetHorizontalHitbox;
    [SerializeField] private Vector2 horizontalHitboxSize;
    [SerializeField] private Vector2 offsetVerticalHitbox;
    [SerializeField] private Vector2 verticalHitboxSize;
    [SerializeField] private string attackSideAnimName;
    [SerializeField] private string attackUpAnimName;
    [SerializeField] private string attackDownAnimName;

    public bool drawGizmos;

    protected override void Awake()
    {
        base.Awake();
        enemyAlreadyTouch = new List<EnemyController>();
    }

    private float GetAnimationLength(string anim)
    {
        if (animator.GetAnimationLength(anim, out float length))
            return length;
        return 0.5f;
    }

    protected override void Launch()
    {
        base.Launch();
        playerAnimator = fightController.GetComponent<PlayerAnimator>();

        Side side = playerAnimator.currentSide;
        playerAnimator.enableBehaviour = false;

        switch (side)
        {
            case Side.up:
                currentOffset = offsetVerticalHitbox;
                currentSize = verticalHitboxSize;
                attackDuration = GetAnimationLength(attackUpAnimName);
                break;
            case Side.down:
                currentOffset = new Vector2(offsetVerticalHitbox.x, -offsetVerticalHitbox.y);
                currentSize = verticalHitboxSize;
                attackDuration = GetAnimationLength(attackDownAnimName);
                break;
            case Side.left:
                currentOffset = new Vector2(-offsetHorizontalHitbox.x, offsetHorizontalHitbox.y);
                currentSize = horizontalHitboxSize;
                attackDuration = GetAnimationLength(attackSideAnimName);
                break;
            case Side.right:
                currentOffset = offsetHorizontalHitbox;
                currentSize = horizontalHitboxSize;
                attackDuration = GetAnimationLength(attackSideAnimName);
                break;
            default:
                break;
        }

        isAttackEnable = true;
        lastTimeBeginAttack = Time.time;
    }

    protected override void Update()
    {
        base.Update();
        if (!isAttackEnable)
            return;

        if(Time.time - lastTimeBeginAttack > attackDuration)
        {
            playerAnimator.enableBehaviour = true;
            isAttackEnable = false;
            enemyAlreadyTouch.Clear();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
            return;

        Hitbox.GizmosDraw((Vector2)transform.position + offsetHorizontalHitbox, horizontalHitboxSize, Color.green);
        Hitbox.GizmosDraw((Vector2)transform.position + offsetVerticalHitbox, verticalHitboxSize, Color.green);
    }
}
