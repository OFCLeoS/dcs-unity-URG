using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Attack",
    story: "[Agent] attacks",
    description: "Agent uses its currently selected weapon to Attack.",
    category: "Action",
    id: "cb288e184679a26423f07e1a221caec3")]
public partial class AttackTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    AttackController _attackController;

    AttackController AttackController
    {
        get
        {
            if (_attackController == null) _attackController = Agent.Value.GetComponent<AttackController>();
            return _attackController;
        }
    }

    protected override Status OnStart()
    {
        AttackController.Attack();
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

