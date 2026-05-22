using UnityEngine;
using TMPro;

public abstract class ElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] protected MapGenerator mapGenerator;
    [SerializeField] protected ElevatorDoorsController elevatorDoorsController;

    public abstract string InteractText { get; }

    protected Collider elevatorButtonCollider;

    protected virtual void Awake()
    {
        elevatorButtonCollider = GetComponent<Collider>();
    }

    public virtual void OnInteract(Player player)
    {
        elevatorDoorsController.CloseDoorSequence();
    }
}
