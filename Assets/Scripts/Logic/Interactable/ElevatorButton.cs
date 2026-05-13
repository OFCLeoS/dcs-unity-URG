using UnityEngine;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] Elevator elevator;
    [SerializeField] MapGenerator mapGenerator;


    public void OnInteract(Player player)
    {
        elevator.StartElevatorSequence(player);
        mapGenerator.GenerateMap();
    }
}
