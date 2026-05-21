using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ElevatorDoorsController : MonoBehaviour
{
    [SerializeField] Transform leftDoor;
    [SerializeField] Transform rightDoor;
    [SerializeField] BoxCollider elevatorBlocker;

    [SerializeField] Vector3 leftDoorOpenedPoint;
    [SerializeField] Vector3 leftDoorClosedPoint;

    [SerializeField] Vector3 rightDoorOpenedPoint;
    [SerializeField] Vector3 rightDoorClosedPoint;

    bool active;

    Vector3 leftDoorStart;
    Vector3 leftDoorEnd;

    Vector3 rightDoorStart;
    Vector3 rightDoorEnd;

    [SerializeField] float actionTime = 2;
    float timeLeftForAction;

    void Awake()
    {
        elevatorBlocker.enabled = false;
        enabled = false;
    }

    public void OpenDoorSequence()
    {
        active = true;
        enabled = true;

        leftDoorStart = leftDoorClosedPoint;
        leftDoorEnd = leftDoorOpenedPoint;

        rightDoorStart = rightDoorClosedPoint;
        rightDoorEnd = rightDoorOpenedPoint;

        timeLeftForAction = actionTime;

        elevatorBlocker.enabled = true;
    }

    public void CloseDoorSequence()
    {
        active = true;
        enabled = true;

        leftDoorStart = leftDoorOpenedPoint;
        leftDoorEnd = leftDoorClosedPoint;

        rightDoorStart = rightDoorOpenedPoint;
        rightDoorEnd = rightDoorClosedPoint;

        timeLeftForAction = actionTime;

        elevatorBlocker.enabled = true;
    }

    void HandleElevatorDoors()
    {
        timeLeftForAction -= Time.deltaTime;
        leftDoor.transform.localPosition = Vector3.Slerp(leftDoorStart, leftDoorEnd, 1 - (timeLeftForAction / actionTime));
        rightDoor.transform.localPosition = Vector3.Slerp(rightDoorStart, rightDoorEnd, 1 - (timeLeftForAction / actionTime));
        if (timeLeftForAction <= 0)
        {
            elevatorBlocker.enabled = false;
            active = false;
            enabled = false;
        }
    }

    void Update()
    {
        if (!active)
        {
            Debug.LogError("Elevator Doors was enabled, but is not active. Disabling...");
            enabled = false;
            return;
        }
        HandleElevatorDoors();
    }
}