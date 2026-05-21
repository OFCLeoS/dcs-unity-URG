using UnityEngine;

public class HumanoidAnimationController : MonoBehaviour
{
    protected static readonly int isMovingBool = Animator.StringToHash("isMoving");
    protected static readonly int xMovementFloat = Animator.StringToHash("xMovement");
    protected static readonly int yMovementFloat = Animator.StringToHash("yMovement");

    protected static readonly int dieTrigger = Animator.StringToHash("die");

    protected static readonly int attackTrigger = Animator.StringToHash("attack");

    [SerializeField] protected Animator animator;
    protected float _maxXSpeed;
    protected float _maxZSpeed;

    #region Initialization
    public void Initalize(float maxXSpeed, float maxZSpeed)
    {
        _maxXSpeed = maxXSpeed;
        _maxZSpeed = maxZSpeed;
    }
    #endregion

    public void PlayerDeathAnimation()
    {
        animator.SetTrigger(dieTrigger);
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(attackTrigger);
    }

    public void ChangeAnimatorController(RuntimeAnimatorController newAnimatorController)
    {
        animator.runtimeAnimatorController = newAnimatorController;
    }

    void Update()
    {

    }
}