using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Move To",
    story: "[Agent] moves to [Target]",
    description: "Moves to the position the Transform was on when the node was first ran.",
    category: "Action/Navigation",
    id: "fb2e129544476f7a300dc7b7b585cb37")]
public partial class MoveToAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    AIMovementModule _movementModule;

    AIMovementModule MovementModule
    {
        get
        {
            if (_movementModule == null) _movementModule = Agent.Value.GetComponent<AIMovementModule>();
            return _movementModule;
        }
    }

    protected override Status OnStart()
    {
        MovementModule.SetDestination(Target.Value.position);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (MovementModule.GetTargetDestination() != Target.Value.position) return Status.Failure;

        if (MovementModule.ReachedDestination()) return Status.Success;
        else return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

