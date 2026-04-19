using Unity.Behavior;
using Unity.Behavior.GraphFramework;
using UnityEngine;

/// <summary>
/// Used for easier controlling of an Agent's Behaviour Graph
/// </summary>
public class AIBehaviourGraphManager : MonoBehaviour
{
    BehaviorGraphAgent agentBehaviourGraph;

    #region Blackboard References
    SerializableGUID missionGUID;
    SerializableGUID targetGUID;
    SerializableGUID playerGUID;
    SerializableGUID playerCloseGUID;
    #endregion

    #region Initialization
    void Awake()
    {
        CheckForAgentBehaviourGraph();
        if (!agentBehaviourGraph.GetVariableID("Mission", out missionGUID))
        {
            throw new BlackboardVariableNotFoundException("Mission");
        }
        if (!agentBehaviourGraph.GetVariableID("Target", out targetGUID))
        {
            throw new BlackboardVariableNotFoundException("Target");
        }
        if (!agentBehaviourGraph.GetVariableID("Player", out playerGUID))
        {
            throw new BlackboardVariableNotFoundException("Player");
        }
        if (!agentBehaviourGraph.GetVariableID("Player Close", out playerCloseGUID))
        {
            throw new BlackboardVariableNotFoundException("Player Close");
        }
    }

    void CheckForAgentBehaviourGraph()
    {
        if (!agentBehaviourGraph)
        {
            agentBehaviourGraph = GetComponent<BehaviorGraphAgent>();
            if (!agentBehaviourGraph)
            {
                Debug.LogError("An Agent Behaviour Graph was not found in the \"" + name + "\" Behaviour Graph Manager. The Behaviour Graph Manager will not function.");
                enabled = false;
            }
        }
    }
    #endregion

    public void SetAgentMission(Mission mission) => agentBehaviourGraph.SetVariableValue(missionGUID, mission);
    public void SetTarget(Transform target) => agentBehaviourGraph.SetVariableValue(targetGUID, target);
    public void SetPlayer(Transform player) => agentBehaviourGraph.SetVariableValue(playerGUID, player);
    public void SetPlayerCloseBool(bool playerClose) => agentBehaviourGraph.SetVariableValue(playerCloseGUID, playerClose);
}
