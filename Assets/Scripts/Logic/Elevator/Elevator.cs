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
    bool endSequence = false;

    Vector3 startPoint;
    Vector3 endPoint;

    Vector3 elevatorTPPoint;
    bool playerStickingToElevator;

    [Header("Fading")]
    [SerializeField] FadeScreen fadeScreen;
    bool fadeInCompleted = true;
    bool fadeOutCompleted = false;
    [SerializeField] float fadeInTime = 2;
    [SerializeField] float fadeOutTime = 2;

    [SerializeField] float shakeIntensity = 0.5f;

    #region Y Pos Sticking
    Transform player;
    CharacterController playerCharacterController;
    CameraShaker playerCameraShaker;
    #endregion

    // TODO: VERY MESSY PROCESS, REFACTOR!

    public void StartElevatorSequence(Player player, bool goUp)
    {
        endSequence = false;
        fadeInCompleted = true;
        fadeOutCompleted = false;
        playerStickingToElevator = false;
        fadeScreen.StartFade(fadeOutTime);

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
            playerCameraShaker = this.player.GetComponent<CameraShaker>();
        }
    }

    public void StopElevatorSequence(Vector3 stopPosition)
    {
        endSequence = true;

        elevatorTPPoint = stopPosition;

        fadeInCompleted = true;
        fadeOutCompleted = false;

        fadeScreen.StartFade(fadeOutTime);
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
        transform.localPosition = Vector3.Lerp(startPoint, endPoint, elevatorProgress / _elevatorLoopTime);
        if (elevatorProgress >= _elevatorLoopTime)
        {
            elevatorProgress = 0;
        }
    }

    /// <summary>
    /// </summary>
    /// <returns>True if Fde Out sequence is fully complete</returns>
    bool HandleFadeOutSequence()
    {
        if (!fadeOutCompleted)
        {
            if (fadeScreen.FadeOutStep(Time.deltaTime))
            {
                fadeOutCompleted = true;
                fadeScreen.StartFade(fadeInTime);
                return true;
            }
            return false;
        }
        else return true;
    }

    /// <summary>
    /// </summary>
    /// <returns>True if Fade In sequence is fully complete</returns>
    bool HandleFadeInSequence()
    {
        if (!fadeInCompleted)
        {
            if (fadeScreen.FadeInStep(Time.deltaTime))
            {
                fadeInCompleted = true;
                return true;
            }
            return false;
        }
        else return true;
    }

    void HandleElevator()
    {
        ProgressElevatorState();
        StickPlayerToElevatorFloor();
    }

    /// <summary>
    /// Handles the fading 
    /// </summary>
    void HandleStopSequence()
    {
        if (HandleFadeOutSequence())
        {
            active = false;
            Player player = this.player.GetComponent<Player>();
            player.CameraDriver.ResetCameraSettings();
            playerCharacterController.enabled = false;
            player.transform.position = elevatorTPPoint;
            playerCharacterController.enabled = true;
            player.CrosshairController.ResetCrosshairPosition();

            fadeInCompleted = false;
            fadeScreen.StartFade(fadeInTime);
        }
        else
        {
            HandleElevator();
        }
    }

    void Update()
    {
        if (!fadeInCompleted) HandleFadeInSequence(); // This is not ideal at all
        if (active)
        {
            if (!playerStickingToElevator)
            {
                if (HandleFadeOutSequence())
                {
                    Player player = this.player.GetComponent<Player>();
                    AdaptCameraSettings(player.CameraDriver);
                    playerCharacterController.enabled = false;
                    player.transform.position = elevatorFloor.position + (Vector3.up * (playerCharacterController.height / 2.0f));
                    playerCharacterController.enabled = true;
                    player.CrosshairController.ResetCrosshairPosition();
#if UNITY_EDITOR
                    if (DEBUG_ACTIVE) DEBUG_SIMULATION_ACTIVE = true;
#endif
                    playerStickingToElevator = true;

                    fadeInCompleted = false;
                    fadeScreen.StartFade(fadeInTime);
                }
            }
            else
            {
                playerCameraShaker.ShakeCamera(0.1f,shakeIntensity);
                if (!endSequence)
                {
                    HandleElevator();
                }
                else
                {
                    HandleStopSequence();
                }
            }
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
    [Header("DEBUG")]
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