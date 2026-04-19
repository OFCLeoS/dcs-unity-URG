using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Module that allows custom behaviour upon issuing movement commands to an Agent
/// </summary>
[RequireComponent(typeof(AIMovementModule))]
public class AIMovementModule : MonoBehaviour
{
    const float DEFAULT_STOPPING_DISTANCE = 0.01f;

    NavMeshAgent navMeshAgent;
    [SerializeField] float speed;

    #region Initialization
    void Awake() => InitializeMovementModule();

    void InitializeMovementModule()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // The rotation will be updated by this module
        navMeshAgent.updateRotation = false;
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = DEFAULT_STOPPING_DISTANCE;
    }
    #endregion

    public void SetStoppingDistance(float stoppingDistance) => navMeshAgent.stoppingDistance = stoppingDistance;

    /// <summary>
    /// Gives the Agent a new Destination.
    /// </summary>
    public void SetDestination(Vector3 destination)
    {
        Debug.Log(navMeshAgent.stoppingDistance);
        navMeshAgent.SetDestination(destination);
    }

    /// <summary>
    /// </summary>
    /// <returns>True if the Agent has reached it's destination</returns>
    public bool ReachedDestination()
    {
        Vector3 destination = navMeshAgent.destination;

        float distance = Vector3.Distance(new Vector3(transform.position.x, destination.y, transform.position.z), destination);
        bool destinationReached = distance <= navMeshAgent.stoppingDistance;

        Debug.Log("Stopping: " + navMeshAgent.stoppingDistance);
        Debug.Log("Distance: " + distance);

        if (destinationReached && !navMeshAgent.pathPending) return true;
        else return false;
    }

    /// <summary>
    /// </summary>
    /// <returns>True if the Agent has reached the given target</returns>
    public bool ReachedTarget(Vector3 target)
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, target.y, transform.position.z), target);
        return distance <= navMeshAgent.stoppingDistance;
    }

    /// <summary>
    /// </summary>
    /// <returns>The current target destination of the Agent</returns>
    public Vector3 GetTargetDestination() => navMeshAgent.destination;

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
