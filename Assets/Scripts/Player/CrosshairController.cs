using UnityEngine;

/// <summary>
/// Controller that allows crosshair manipulation
/// </summary>
public class CrosshairController : MonoBehaviour
{
    [SerializeField] Transform crosshair;
    [Tooltip("How far the crosshair can be from the player's feet transform on each axis (X and Z).")]
    [SerializeField] float maximumXDistanceFromPlayerPerAxis = 25;
    [SerializeField] float maximumZDistanceFromPlayerPerAxis = 25;
    [Tooltip("Player Feet location to ground the crosshair")]
    [SerializeField] Transform playerFeet;
    Vector2 normalizedCrosshairPosition;
    public Vector2 NormalizedCrosshairPosition { get { return normalizedCrosshairPosition; } }

    #region Initialization
    void Awake()
    {
        HideCursor();
        ResetCrosshairPosition();
    }
    #endregion

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public Vector3 GetDirectionToCrosshair(Vector3 fromPos)
    {
        return crosshair.position - fromPos;
    }

    /// <summary>
    /// Sets the Crosshair position to the player position
    /// </summary>
    public void ResetCrosshairPosition()
    {
        crosshair.position = playerFeet.position;
        normalizedCrosshairPosition = Vector2.one * 0.5f;
    }

    /// <summary>
    /// Moves the crosshair whilst limiting its position depending on the crosshair settings
    /// </summary>
    /// <param name="movement"></param>
    public void MoveCrosshair(Vector2 movement)
    {
        float newXPos = crosshair.position.x + (movement.x * Time.deltaTime);
        float deltaX = crosshair.position.x - playerFeet.transform.position.x;
        normalizedCrosshairPosition.x = Mathf.Clamp(deltaX / maximumXDistanceFromPlayerPerAxis, -1, 1);
        if (Mathf.Abs(deltaX) > maximumXDistanceFromPlayerPerAxis)
        {
            if (deltaX < 0)
            {
                newXPos = playerFeet.transform.position.x - maximumXDistanceFromPlayerPerAxis;
            }
            else
            {
                newXPos = playerFeet.transform.position.x + maximumXDistanceFromPlayerPerAxis;
            }
        }
        float newZPos = crosshair.position.z + (movement.y * Time.deltaTime);
        float deltaZ = crosshair.position.z - playerFeet.transform.position.z;
        normalizedCrosshairPosition.y = Mathf.Clamp(deltaZ / maximumZDistanceFromPlayerPerAxis, -1, 1);
        if (Mathf.Abs(deltaZ) > maximumZDistanceFromPlayerPerAxis)
        {
            if (deltaZ < 0)
            {
                newZPos = playerFeet.transform.position.z - maximumZDistanceFromPlayerPerAxis;
            }
            else
            {
                newZPos = playerFeet.transform.position.z + maximumZDistanceFromPlayerPerAxis;
            }
        }
        crosshair.position = new Vector3(newXPos, playerFeet.position.y, newZPos);
    }
}