using Unity.Behavior;
using Unity.Behavior.GraphFramework;
using UnityEngine;

/// <summary>
/// Module that allows an Agent to check whether a target is close enough to attack
/// </summary>
public class AITargetProximityMonitor : MonoBehaviour
{
    #region Blackboard References
    SerializableGUID targetCloseGUID;
    #endregion
    bool targetClose = false;

    BehaviorGraphAgent agentBehaviourTree;

    [SerializeField] Transform target;

    [Tooltip("How close the target has to be from the Agent to be considered close enough")]
    [SerializeField] float _closeThreshold = 1.39f;
    /// <summary>
    /// closeThreshold squared for optimization purposes
    /// </summary>
    float _squaredCloseThreshold;

    #region Initialization
    void Awake()
    {
        _squaredCloseThreshold = _closeThreshold * _closeThreshold;
        agentBehaviourTree = GetComponent<BehaviorGraphAgent>();
        if (!agentBehaviourTree.GetVariableID("Target Close", out targetCloseGUID))
        {
            throw new BlackboardVariableNotFoundException("Target Close");
        }
    }
    #endregion

    void CheckTargetProximity()
    {
        targetClose = (target.position - transform.position).sqrMagnitude <= _squaredCloseThreshold;
        agentBehaviourTree.SetVariableValue(targetCloseGUID, targetClose);
    }

    #region Performance
    const float MIN_CHECK_TIME = 0.3f;
    const float MAX_CHECK_TIME = 0.6f;
    float timeSinceLastCheck = 0;
    float nextCheckTime = 0;
    #endregion

    void Update()
    {
        timeSinceLastCheck += Time.deltaTime;
        if (timeSinceLastCheck >= nextCheckTime)
        {
            CheckTargetProximity();
            timeSinceLastCheck = 0;
            nextCheckTime = Random.Range(MIN_CHECK_TIME, MAX_CHECK_TIME); // TODO: Use faster library?
        }
    }
}