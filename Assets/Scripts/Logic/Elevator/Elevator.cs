using UnityEngine;


public class Elevator : MonoBehaviour
{
    [SerializeField] Transform elevatorFloor;
    [SerializeField] float _elevatorLoopTime;

    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;

    [Tooltip("How far the camera will be from the player once the sequence starts")]
    [SerializeField] float playerCameraDistance;
    [Tooltip("The rotation the camera will have once the sequence starts")]
    [SerializeField] Vector3 playerCameraRotation;

    bool active;

    #region Y Pos Sticking
    Transform player;
    CharacterController playerCharacterController;
    #endregion

    public void StartElevatorSequence(Player player)
    {
        active = true;
        if (player != this.player)
        {
            this.player = player.transform;
            playerCharacterController = this.player.GetComponent<CharacterController>();
        }
        AdaptCameraSettings(player.CameraDriver);
        playerCharacterController.enabled = false;
        player.transform.position = elevatorFloor.position + (Vector3.up * (playerCharacterController.height / 2.0f));
        playerCharacterController.enabled = true;
        player.CrosshairController.ResetCrosshairPosition();
    }

    /// <summary>
    /// Adapts Camera Settings to the elevator sequence
    /// </summary>
    void AdaptCameraSettings(CameraDriver cameraDriver)
    {
        cameraDriver.DisableSmoothing();
        cameraDriver.SetCameraDistance(playerCameraDistance);
        cameraDriver.SetCameraRotation(playerCameraRotation);
    }

    void StickPlayerToElevatorFloor()
    {
        playerCharacterController.enabled = false;
        player.transform.position = new Vector3(player.transform.position.x, elevatorFloor.position.y + (playerCharacterController.height / 2.0f), player.transform.position.z);
        playerCharacterController.enabled = true;
    }

    float elevatorProgress;
    void ProgressElevatorState()
    {
        elevatorProgress += Time.deltaTime;
        elevatorFloor.localPosition = Vector3.Lerp(startPoint, endPoint, elevatorProgress / _elevatorLoopTime);
        if (elevatorProgress >= _elevatorLoopTime)
        {
            elevatorProgress = 0;
        }
    }

    void HandleElevator()
    {
        ProgressElevatorState();
        StickPlayerToElevatorFloor();
    }

    void Update()
    {
        if (active)
        {
            HandleElevator();
        }
    }
}