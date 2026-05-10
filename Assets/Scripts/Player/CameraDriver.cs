using UnityEngine;

/// <summary>
/// Driver responsible to make the camera follow the player
/// </summary>
public class CameraDriver : MonoBehaviour
{
    [SerializeField] Transform player;
    Quaternion startingRotation;

    [Tooltip("How far the camera will be from the player.")]
    [SerializeField] float cameraYDistance;

    bool followPlayer;

    Transform alternateTarget;

    #region Initialization
    void Awake()
    {
        followPlayer = true;
        startingRotation = transform.rotation;
    }
    #endregion

    /// <summary>
    /// Makes the camera Stick to a target transform
    /// </summary>
    public void StickToTarget(Transform target)
    {
        alternateTarget = target;
        followPlayer = false;
        transform.rotation = target.rotation;
    }

    public void FollowPlayer()
    {
        followPlayer = true;
        transform.rotation = startingRotation;
    }

    void HandleDriver()
    {
        if (followPlayer)
        {
            transform.position = new Vector3(player.position.x, player.position.y + cameraYDistance, player.position.z);
        }
        else
        {
            transform.position = new Vector3(alternateTarget.position.x, alternateTarget.position.y, alternateTarget.position.z);
        }
    }

    void Update()
    {
        HandleDriver();
    }
}
