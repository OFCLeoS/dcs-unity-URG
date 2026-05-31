using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Follow Target",
    story: "[Agent] follows [Target]",
    description: "Follows a Transform, returns Success when reached.",
    category: "Action/Navigation",
    id: "c541cc9e6d07d39bbcb0054f19bf6241")]
public partial class FollowTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    [SerializeReference] public BlackboardVariable<float> StoppingDistance = new BlackboardVariable<float>(1.5f);

    AIFollowModule _followModule;

    AIFollowModule FollowModule
    {
        get
        {
            if (_followModule == null) _followModule = Agent.Value.GetComponent<AIFollowModule>();
            return _followModule;
        }
    }

    protected override Status OnStart()
    {
        FollowModule.SetStoppingDistance(StoppingDistance);
        FollowModule.SetFollowTarget(Target.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // TODO: ADD THIS ONCE IT WORKS!
        //if(!FollowModule.IsFollowingTarget()) return Status.Failure;

        if (FollowModule.ReachedFollowTarget()) return Status.Success;
        else return Status.Running;
    }

    protected override void OnEnd() => FollowModule.StopFollowing();
}

