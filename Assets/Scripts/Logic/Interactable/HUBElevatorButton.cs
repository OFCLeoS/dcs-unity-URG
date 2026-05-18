using UnityEngine;

public class HUBElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] MapGenerator mapGenerator;

    Collider elevatorButtonCollider;
    void Awake()
    {
        elevatorButtonCollider = GetComponent<Collider>();
    }

    public void DisableElevatorButton()
    {
        elevatorButtonCollider.enabled = false;
    }

    public void EnableElevatorButton()
    {
        elevatorButtonCollider.enabled = true;
    }

    public void OnInteract(Player player)
    {
        mapGenerator.StartMapGeneration(player);
        DisableElevatorButton();
    }
}
