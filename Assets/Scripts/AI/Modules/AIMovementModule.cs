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
        navMeshAgent.SetDestination(destination);
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
