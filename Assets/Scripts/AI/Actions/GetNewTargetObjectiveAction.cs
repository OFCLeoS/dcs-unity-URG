using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Get new Target Objective",
    story: "[Agent] gets a new Defend Objective as its [Target] if possible",
    description: "Finds a new Defend Objective if possible, and assigns it to Target. Will set the Mission to \"Hunt\" if no Defend Objective is available.",
    category: "Action",
    id: "5eba07b911db25e45d0ab4436f8fe59e")]
public partial class GetNewTargetObjectiveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    [SerializeReference] public BlackboardVariable<bool> ValidTarget;

    AIAgent _agent;

    AIAgent AIAgent
    {
        get
        {
            if (_agent == null) _agent = Agent.Value.GetComponent<AIAgent>();
            return _agent;
        }
    }

    protected override Status OnStart()
    {
        AIAgent.UpdateAgentObjective();
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

