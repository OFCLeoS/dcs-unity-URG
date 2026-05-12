using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRotation : MonoBehaviour
{
    InputAction mouseDeltaAction;

    [SerializeField] float mouseSensitivity = 1;
    [SerializeField] CrosshairController crosshairController;

    #region Initialization
    void Awake()
    {
        mouseDeltaAction = InputSystem.actions.FindAction("Look");
        if (!crosshairController)
        {
            crosshairController = GetComponent<CrosshairController>();
        }
    }
    #endregion


    void HandleRotation()
    {
        Vector2 mouseDelta = mouseDeltaAction.ReadValue<Vector2>();
        crosshairController.MoveCrosshair(mouseDelta * mouseSensitivity);
    
        transform.rotation = Quaternion.LookRotation(crosshairController.GetDirectionToCrosshair(transform.position), transform.up);
    }

    // TODO: PERFORMANCE CHECK
    void Update()
    {
        HandleRotation();
    }
}
