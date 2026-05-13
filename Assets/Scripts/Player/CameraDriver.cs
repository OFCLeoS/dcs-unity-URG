using Cinemachine;
using UnityEngine;

/// <summary>
/// Driver responsible to make the camera follow the player
/// </summary>
public class CameraDriver : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CrosshairController crosshairController;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [SerializeField] float screenXMin = 0.1f;
    [SerializeField] float screenXMax = 0.9f;

    [SerializeField] float screenYMin = 0.1f;
    [SerializeField] float screenYMax = 0.9f;

    CinemachineFramingTransposer virtualCameraBody;


    #region Initialization
    void Awake()
    {
        virtualCamera.Follow = player;
        virtualCameraBody = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    #endregion


    void HandleDriver()
    {
        Vector2 normalizedCrosshairPosition = crosshairController.NormalizedCrosshairPosition;

        virtualCameraBody.m_ScreenX = Mathf.Lerp(screenXMin, screenXMax, Mathf.InverseLerp(-1f, 1f, -normalizedCrosshairPosition.x));
        virtualCameraBody.m_ScreenY = Mathf.Lerp(screenYMin, screenYMax, Mathf.InverseLerp(-1f, 1f, normalizedCrosshairPosition.y));
    }

    void Update()
    {
        HandleDriver();
    }
}
