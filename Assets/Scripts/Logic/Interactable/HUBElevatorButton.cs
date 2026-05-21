using UnityEngine;

public class HUBElevatorButton : ElevatorButton
{
    public override string InteractText => "Start Wave";

    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
        mapGenerator.StartMapGeneration(player);
        elevatorButtonCollider.enabled = false;
    }
}
