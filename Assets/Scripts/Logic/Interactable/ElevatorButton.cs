using UnityEngine;

public class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] Elevator elevator;


    public void OnInteract(Player player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = elevator.transform.position;
        elevator.StartElevatorSequence();
        player.GetComponent<CharacterController>().enabled = true;
    }

}
