using UnityEngine;

/// <summary>
/// Driver responsible to make the camera follow the player
/// </summary>
public class CameraDriver : MonoBehaviour
{
    [SerializeField] Transform player;

    void HandleDriver()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    void Update()
    {
        HandleDriver();
    }
}
