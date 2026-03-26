using UnityEngine;

/// <summary>
/// Module that allows an Agent to follow a Transform Target
/// </summary>
[RequireComponent(typeof(AIMovementModule))]
public class AIFollowModule : MonoBehaviour
{
    AIMovementModule movementModule;

    [SerializeField] Transform target;
    [SerializeField] float stoppingDistance;

    #region Initialization
    void Awake()
    {
        movementModule = GetComponent<AIMovementModule>();
    }
    #endregion
    
    public void SetFollowTarget(Transform target)
    {
        movementModule.SetStoppingDistance(stoppingDistance);
        this.target = target;
    }

    void FollowTarget()
    {
        if (target != null)
        {
            movementModule.SetDestination(target.position);
        }
    }

    void Update()
    {
        FollowTarget();
    }
}
