using UnityEngine;


public class Elevator : MonoBehaviour
{
    [SerializeField] Transform elevatorFloor;
    [SerializeField] float _elevatorLoopTime;

    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;

    bool active;

    public void StartElevatorSequence()
    {
        active = true;
    }

    float elevatorProgress;
    void Update()
    {
        if (active)
        {
            elevatorProgress += Time.deltaTime;
            elevatorFloor.localPosition = Vector3.Lerp(startPoint, endPoint, elevatorProgress / _elevatorLoopTime);
            if (elevatorProgress >= _elevatorLoopTime)
            {
                elevatorProgress = 0;
            }
        }
    }
}