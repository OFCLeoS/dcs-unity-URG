using UnityEngine;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] Elevator elevator;


    public void OnInteract(Player player)
    {
        elevator.StartElevatorSequence(player);
    }
}
