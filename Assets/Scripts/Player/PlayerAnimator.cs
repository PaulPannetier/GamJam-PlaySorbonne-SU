using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private CharController characterController;
    private Animator animator;
    private int runAnim, runUpAnim, runDownAnim, idleAnim;

    [SerializeField] private string runAnimationName;
    [SerializeField] private string runUpAnimationName;
    [SerializeField] private string runDownAnimationName;
    [SerializeField] private string idleAnimationName;

    private void Awake()
    {
        characterController = GetComponent<CharController>();
        animator = GetComponent<Animator>();
        runAnim = Animator.StringToHash(runAnimationName);
        runUpAnim = Animator.StringToHash(runUpAnimationName);
        runDownAnim = Animator.StringToHash(runDownAnimationName);
        idleAnim = Animator.StringToHash(idleAnimationName);
    }

    private void Update()
    {
        Vector2 speed = characterController.GetCurrentSpeed();
        if(speed.magnitude < 1e-3f)
        {
            animator.CrossFade(idleAnim, 0f, 0);
            return;
        }

        if(Mathf.Abs(speed.x) >= Mathf.Abs(speed.y * 0.95f))
        {
            animator.CrossFade(runAnim, 0f, 0);
        }
        else
        {
            int anim = speed.y > 0f ? runUpAnim : runDownAnim;
            animator.CrossFade(anim, 0f, 0);
        }
    }
}
