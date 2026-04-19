using Unity.Behavior;
using Unity.Behavior.GraphFramework;
using UnityEngine;

/// <summary>
/// Module that allows an Agent to check whether the player is close enough to attack
/// </summary>
[RequireComponent(typeof(AIBehaviourGraphManager))]
public class AITargetProximityMonitor : MonoBehaviour
{
    AIBehaviourGraphManager behaviourGraphManager;

    bool playerClose = false;

    Transform player;

    [Tooltip("How close the player has to be from the Agent to be considered close enough to attack")]
    [SerializeField] float _closeThreshold = 1.39f;
    /// <summary>
    /// closeThreshold squared for optimization purposes
    /// </summary>
    float _squaredCloseThreshold;

    #region Initialization
    void Awake()
    {
        behaviourGraphManager = GetComponent<AIBehaviourGraphManager>();
        _squaredCloseThreshold = _closeThreshold * _closeThreshold;
        enabled = false;
    }

    public void Initialize(Transform player)
    {
        this.player = player;
        enabled = true;
    }
    #endregion

    void CheckTargetProximity()
    {
        playerClose = (player.position - transform.position).sqrMagnitude <= _squaredCloseThreshold;
        behaviourGraphManager.SetPlayerCloseBool(playerClose);
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