using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Module that allows custom behaviour upon issuing movement commands to an Agent
/// </summary>
[RequireComponent(typeof(AIAgent), typeof(AIMovementModule))]
public class AIMovementModule : MonoBehaviour
{
    const float DEFAULT_STOPPING_DISTANCE = 1.5f;

    float squared_stopping_distance;
    NavMeshAgent navMeshAgent;
    [SerializeField] float speed;

    #region Initialization
    void Awake() => InitializeMovementModule();

    void InitializeMovementModule()
    {
        squared_stopping_distance = DEFAULT_STOPPING_DISTANCE * DEFAULT_STOPPING_DISTANCE;
        navMeshAgent = GetComponent<NavMeshAgent>();
        // We want the agent to be able to immidiately reach their desired speed
        navMeshAgent.acceleration = 99999999999;
        navMeshAgent.autoBraking = false;
        // TODO: SET TO FALSE (maybe): The rotation will be updated by this module
        navMeshAgent.updateRotation = true;
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = DEFAULT_STOPPING_DISTANCE;
    }
    #endregion

    public void SetMovementSpeed(float newSpeed)
    {
        speed = newSpeed;
        navMeshAgent.speed = speed;
    }

    public void SetStoppingDistance(float stoppingDistance)
    {
        navMeshAgent.stoppingDistance = stoppingDistance;
        squared_stopping_distance = stoppingDistance * stoppingDistance;
    }

    /// <summary>
    /// Gives the Agent a new Destination.
    /// </summary>
    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }

    /// <summary>
    /// </summary>
    /// <returns>True if the Agent has reached it's destination</returns>
    public bool ReachedDestination()
    {
        Vector3 destination = navMeshAgent.destination;

        float sqrMagnitude = (new Vector3(transform.position.x, destination.y, transform.position.z) - destination).sqrMagnitude;
        bool destinationReached = sqrMagnitude <= squared_stopping_distance;

        if (destinationReached && !navMeshAgent.pathPending) return true;
        else return false;
    }

    /// <summary>
    /// </summary>
    /// <returns>True if the Agent has reached the given target</returns>
    public bool ReachedTarget(Vector3 target)
    {
        float sqrMagnitude = (new Vector3(transform.position.x, target.y, transform.position.z) - target).sqrMagnitude;
        return sqrMagnitude <= squared_stopping_distance;
    }

    /// <summary>
    /// </summary>
    /// <returns>True if the Agent destination is the same as the provided one (y-Axis ignored)</returns>
    public bool IsAgentDestination(Vector3 destination)
    {
        return navMeshAgent.destination.x == destination.x && navMeshAgent.destination.z == destination.z;
    }

    /// <summary>
    /// Makes the Agent stop going towards their previous destination.
    /// </summary>
    public void ClearDestination()
    {
        if (navMeshAgent.isActiveAndEnabled) navMeshAgent.ResetPath();
    }

    public void DisableMovementModule()
    {
        navMeshAgent.enabled = false;
    }

# if UNITY_EDITOR
    #region DEBUG
    public Vector3 DEBUG_POS;
    [ContextMenu("GOTO POS")]
    public void DEBUG_GOTO()
    {
        SetDestination(DEBUG_POS);
    }
    #endregion
#endif
}
