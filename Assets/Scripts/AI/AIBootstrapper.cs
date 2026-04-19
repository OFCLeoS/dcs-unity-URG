using UnityEngine;

/// <summary>
/// In charge of correctly initializing components of an Agent that require special Initialization that "Awake" or "Start" cannot provide.
/// </summary>
public class AIBootstrapper : MonoBehaviour
{
    public void Initialize(Transform player)
    {
        AITargetProximityMonitor targetProximityMonitor = GetComponent<AITargetProximityMonitor>();
        if (targetProximityMonitor) InitializeTargetProximityMonitor(targetProximityMonitor, player);
        AIBehaviourGraphManager behaviourGraphManager = GetComponent<AIBehaviourGraphManager>();
        if(behaviourGraphManager) InitializeDefaultBehaviourGraphValues(behaviourGraphManager,player);
        
        // This component is only required for initializing an Agent
        Destroy(this);
    }

    void InitializeTargetProximityMonitor(AITargetProximityMonitor targetProximityMonitor, Transform player)
    {
        targetProximityMonitor.Initialize(player);
    }

    void InitializeDefaultBehaviourGraphValues(AIBehaviourGraphManager behaviourGraphManager, Transform player)
    {
        behaviourGraphManager.SetPlayer(player);
    }
}