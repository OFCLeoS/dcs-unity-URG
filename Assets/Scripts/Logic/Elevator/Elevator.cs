using UnityEngine;


public class Elevator : MonoBehaviour
{
    [SerializeField] Transform elevatorFloor;
    [SerializeField] float _elevatorLoopTime;

    [SerializeField] Vector3 _defaultStartPoint;
    [SerializeField] Vector3 _defaultEndPoint;

    [Tooltip("How far the camera will be from the player once the sequence starts")]
    [SerializeField] float playerCameraDistance;
    [Tooltip("The rotation the camera will have once the sequence starts")]
    [SerializeField] Vector3 playerCameraRotation;

    bool active;

    Vector3 startPoint;
    Vector3 endPoint;

    #region Y Pos Sticking
    Transform player;
    CharacterController playerCharacterController;
    #endregion

    public void StartElevatorSequence(Player player, bool goUp)
    {
        if (goUp)
        {
            startPoint = _defaultEndPoint;
            endPoint = _defaultStartPoint;
        }
        else
        {
            startPoint = _defaultStartPoint;
            endPoint = _defaultEndPoint;
        }

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
#if UNITY_EDITOR
        if (DEBUG_ACTIVE) DEBUG_SIMULATION_ACTIVE = true;
#endif
    }

    public void StopElevatorSequence(Vector3 stopPosition)
    {
        active = false;
        Player player = this.player.GetComponent<Player>();
        player.CameraDriver.ResetCameraSettings();
        playerCharacterController.enabled = false;
        player.transform.position = stopPosition;
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
#if UNITY_EDITOR
        if (DEBUG_SIMULATION_ACTIVE)
        {
            DEBUG_SIMULATE_ELEVATOR_SEQUENCE();
        }
#endif
    }

    #region DEBUGGING
#if UNITY_EDITOR
    public bool DEBUG_ACTIVE = false;
    float DEBUG_ELEVATOR_TIME = 5;
    bool DEBUG_SIMULATION_ACTIVE = false;
    public Vector3 DEBUG_STOP_POS;
    void DEBUG_SIMULATE_ELEVATOR_SEQUENCE()
    {
        DEBUG_ELEVATOR_TIME -= Time.deltaTime;
        if (DEBUG_ELEVATOR_TIME <= 0)
        {
            StopElevatorSequence(DEBUG_STOP_POS);
            DEBUG_SIMULATION_ACTIVE = false;
        }
    }
#endif
    #endregion
}