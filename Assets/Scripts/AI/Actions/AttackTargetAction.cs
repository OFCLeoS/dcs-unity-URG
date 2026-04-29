using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Attack Target",
    story: "[Agent] attacks [Target]",
    description: "Attacks a Target. Return Failure if the Target is destroyed or does not exist.",
    category: "Action",
    id: "cb288e184679a26423f07e1a221caec3")]
public partial class AttackTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    Transform oldTarget;

    AttackController _attackController;

    AttackController AttackController
    {
        get
        {
            if (_attackController == null) _attackController = Target.Value.GetComponent<AttackController>();
            return _attackController;
        }
    }

    protected override Status OnStart()
    {
        //AttackController.
        return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

