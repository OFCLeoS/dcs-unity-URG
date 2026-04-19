using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Attack Target",
    story: "[Agent] attacks [Target]",
    description: "Attacks a Target.",
    category: "Action",
    id: "cb288e184679a26423f07e1a221caec3")]
public partial class AttackTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        // TODO ATTACK!
        Debug.Log(Agent.Value.name + " has Attacked " + Target.Value.name + "!");
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

