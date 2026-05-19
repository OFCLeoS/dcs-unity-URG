using UnityEngine;

public class HumanoidPlayerAnimationController : HumanoidAnimationController
{
    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerMovement playerMovement;

    #region Initialization
    void Awake()
    {
        if (!characterController)
        {
            characterController = GetComponent<CharacterController>();
        }
        Initalize(playerMovement.Speed, playerMovement.Speed);
    }
    #endregion


    void UpdatePlayerMovementBlendTree()
    {
        if (characterController.velocity != Vector3.zero)
        {
            animator.SetBool(isMovingBool, true);
            // TODO: CAN MAKE THIS MORE PERFORMANT?
            Vector3 localPlayerVelocity = transform.InverseTransformDirection(characterController.velocity);
            animator.SetFloat(xMovementFloat, localPlayerVelocity.x / _maxXSpeed);
            animator.SetFloat(yMovementFloat, localPlayerVelocity.z / _maxZSpeed);
        }
        else animator.SetBool(isMovingBool, false);
    }

    public void ChangeAnimatorController(RuntimeAnimatorController newAnimatorController)
    {
        animator.runtimeAnimatorController = newAnimatorController;
    }

    void Update()
    {
        UpdatePlayerMovementBlendTree();
    }
}