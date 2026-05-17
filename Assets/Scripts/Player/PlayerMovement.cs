using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction movementDirectionAction;
    [SerializeField] CharacterController controller;
    
    [SerializeField] float speed;
    public float Speed { get { return speed; } }

    [SerializeField] CrosshairController crosshairController;

    #region Initialization
    void Awake()
    {
        movementDirectionAction = InputSystem.actions.FindAction("Move");
        if (!crosshairController)
        {
            crosshairController = GetComponent<CrosshairController>();
        }
    }
    #endregion

    void HandleMovement()
    {
        Vector2 movementAxis = movementDirectionAction.ReadValue<Vector2>().normalized;
        Vector3 move = new Vector3(movementAxis.x * speed * Time.deltaTime, 0, movementAxis.y * speed * Time.deltaTime);
        controller.Move(move);
        // We use velocity so that the crosshair does not move when the player is colliding against something
        crosshairController.MoveCrosshair(new Vector2(controller.velocity.x, controller.velocity.z));
    }

    // TODO: PERFORMANCE CHECK
    void Update()
    {
        HandleMovement();
    }
}
