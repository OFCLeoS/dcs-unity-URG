using UnityEngine;

public class WaveElevatorButton : ElevatorButton
{
    public override string InteractText => "Go to HUB";

    public override void OnInteract(Player player)
    {
        base.OnInteract(player);
        mapGenerator.StartMapDestruction(player);
        elevatorButtonCollider.enabled = false;
    }
}
