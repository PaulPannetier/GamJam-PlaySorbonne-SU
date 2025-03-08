using UnityEngine;

public enum Side : byte
{
    up, down, left, right
}

public class PlayerAnimator : MonoBehaviour
{
    private CharController characterController;
    private Animator animator;
    private int runAnim, runUpAnim, runDownAnim, idleAnim;

    [SerializeField] private string runAnimationName;
    [SerializeField] private string runUpAnimationName;
    [SerializeField] private string runDownAnimationName;
    [SerializeField] private string idleAnimationName;

    [HideInInspector] public bool enableBehaviour;

    public Side currentSide {  get; private set; }

    private void Awake()
    {
        characterController = GetComponent<CharController>();
        animator = GetComponent<Animator>();
        runAnim = Animator.StringToHash(runAnimationName);
        runUpAnim = Animator.StringToHash(runUpAnimationName);
        runDownAnim = Animator.StringToHash(runDownAnimationName);
        idleAnim = Animator.StringToHash(idleAnimationName);
        enableBehaviour = true;
        currentSide = Side.right;
    }

    private void Update()
    {
        if (!enableBehaviour)
            return;

        Vector2 speed = characterController.GetCurrentSpeed();
        if(speed.magnitude < 1e-3f)
        {
            animator.CrossFade(idleAnim, 0f, 0);
            return;
        }

        if(Mathf.Abs(speed.x) >= Mathf.Abs(speed.y * 0.95f))
        {
            animator.CrossFade(runAnim, 0f, 0);
            currentSide = speed.x > 0f ? Side.right : Side.left;
        }
        else
        {
            int anim = speed.y > 0f ? runUpAnim : runDownAnim;
            currentSide = speed.y > 0f ? Side.up : Side.down;
            animator.CrossFade(anim, 0f, 0);
        }
    }
}
