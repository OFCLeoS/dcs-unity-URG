using UnityEngine;

public class HumanoidAnimationController : MonoBehaviour
{
    protected static readonly int isMovingBool = Animator.StringToHash("isMoving");
    protected static readonly int xMovementFloat = Animator.StringToHash("xMovement");
    protected static readonly int yMovementFloat = Animator.StringToHash("yMovement");

    protected static readonly int dieTrigger = Animator.StringToHash("die");

    [SerializeField] protected Animator animator;
    protected float _maxXSpeed;
    protected float _maxZSpeed;

    #region Initialization
    public void Initalize(float maxXSpeed,float maxZSpeed)
    {
        _maxXSpeed = maxXSpeed;
        _maxZSpeed = maxZSpeed;
    }
    #endregion

    public void PlayerDeathAnimation()
    {
        animator.SetBool(dieTrigger,true);
    }

    void Update()
    {
        
    }
}