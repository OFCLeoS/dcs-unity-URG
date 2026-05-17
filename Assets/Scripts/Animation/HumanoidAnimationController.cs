using UnityEngine;

public class HumanoidAnimationController : MonoBehaviour
{
    protected readonly int isMovingBool = Animator.StringToHash("isMoving");
    protected readonly int xMovementFloat = Animator.StringToHash("xMovement");
    protected readonly int yMovementFloat = Animator.StringToHash("yMovement");

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

    

    void Update()
    {
        
    }
}