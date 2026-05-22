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

    public void SetSensitivity(float mouseSensitivity)
    {
        this.mouseSensitivity = mouseSensitivity;
    }

    void HandleRotation()
    {
        Vector2 mouseDelta = mouseDeltaAction.ReadValue<Vector2>();
        crosshairController.MoveCrosshair(mouseDelta * mouseSensitivity);
        Vector3 directionToCrosshair = crosshairController.GetDirectionToCrosshair(transform.position);
        directionToCrosshair.y = 0;

        transform.rotation = Quaternion.LookRotation(directionToCrosshair, transform.up);
    }

    // TODO: PERFORMANCE CHECK
    void Update()
    {
        HandleRotation();
    }
}
