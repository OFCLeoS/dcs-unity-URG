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
    Quaternion defaultCameraRotation;


    [SerializeField] float screenXMin = 0.1f;
    [SerializeField] float screenXMax = 0.9f;

    [SerializeField] float screenYMin = 0.1f;
    [SerializeField] float screenYMax = 0.9f;

    CinemachineFramingTransposer virtualCameraBody;
    float defaultCameraDistance;
    float defaultXDamping;
    float defaultYDamping;
    float defaultZDamping;
    float defaultSoftZoneWidth;
    float defaultSoftZoneHeight;

    Transform stickyTarget;
    bool targetSticking;

    #region Initialization
    void Awake()
    {
        virtualCamera.Follow = player;
        defaultCameraRotation = virtualCamera.transform.rotation;
        virtualCameraBody = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        defaultCameraDistance = virtualCameraBody.m_CameraDistance;
        defaultXDamping = virtualCameraBody.m_XDamping;
        defaultYDamping = virtualCameraBody.m_YDamping;
        defaultZDamping = virtualCameraBody.m_ZDamping;
        defaultSoftZoneWidth = virtualCameraBody.m_SoftZoneWidth;
        defaultSoftZoneHeight = virtualCameraBody.m_SoftZoneHeight;
    }
    #endregion

    /// <summary>
    /// Disables the use of smoothing for the camera (Use e.g.: Elevator sequence)
    /// </summary>
    public void DisableSmoothing()
    {
        virtualCameraBody.m_XDamping = 0;
        virtualCameraBody.m_YDamping = 0;
        virtualCameraBody.m_ZDamping = 0;
        virtualCameraBody.m_SoftZoneWidth = 0;
        virtualCameraBody.m_SoftZoneHeight = 0;
    }

    /// <summary>
    /// Enables the use of smoothing for the camera
    /// </summary>
    public void EnableSmoothing()
    {
        virtualCameraBody.m_XDamping = defaultXDamping;
        virtualCameraBody.m_YDamping = defaultYDamping;
        virtualCameraBody.m_ZDamping = defaultZDamping;
        virtualCameraBody.m_SoftZoneWidth = defaultSoftZoneWidth;
        virtualCameraBody.m_SoftZoneHeight = defaultSoftZoneHeight;
    }

    /// <summary>
    /// Sets the camera rotation to the given one
    /// </summary>
    public void SetCameraRotation(Vector3 newRotation) => virtualCamera.transform.rotation = Quaternion.Euler(newRotation);

    /// <summary>
    /// Sets the camera rotation to the default one
    /// </summary>
    public void ResetCameraRotation() => virtualCamera.transform.rotation = defaultCameraRotation;

    public void SetCameraDistance(float distance) => virtualCameraBody.m_CameraDistance = distance;

    /// <summary>
    /// Sets the camera distance to the default one
    /// </summary>
    public void ResetCameraDistance() => virtualCameraBody.m_CameraDistance = defaultCameraDistance;


    public void ResetCameraSettings()
    {
        ResetCameraRotation();
        EnableSmoothing();
        ResetCameraDistance();
    }

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
