using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIMovementModule))]
public class AIMovementModule : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] float speed;

    #region Initialization
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // The rotation will be updated by this module
        navMeshAgent.updateRotation = false;

    }
    #endregion
}
