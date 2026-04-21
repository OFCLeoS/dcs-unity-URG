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

    IDamageable _damageable;

    IDamageable Damageable
    {
        get
        {
            if (_damageable == null) _damageable = Target.Value.GetComponent<IDamageable>();
            return _damageable;
        }
    }

    protected override Status OnStart()
    {
        if (oldTarget == null) oldTarget = Target.Value;
        else if (oldTarget != Target.Value)
        {
            _damageable = null;
            oldTarget = Target.Value;
        }

        if (!Damageable.IsDestroyed)
        {
            Damageable.TakeDamage(0.075f);
            // TODO: ATTACK LOGIC HERE!
            return Status.Success;
        }
        else return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

