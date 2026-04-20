using UnityEngine;

/// <summary>
/// Module that allows an Agent to follow a Transform Target
/// </summary>
[RequireComponent(typeof(AIAgent), typeof(AIMovementModule))]
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

    public void SetStoppingDistance(float stoppingDistance) => movementModule.SetStoppingDistance(stoppingDistance);

    /// <summary>
    /// Sets the Follow Target to the one provided in the parameter
    /// </summary>
    public void SetFollowTarget(Transform target)
    {
        this.target = target;
    }

    /// <summary>
    /// Sets the destination of the Agent to the target if it has changed
    /// </summary>
    void FollowTarget()
    {
        if (target != null)
        {
            // TODO: OPTIMIZE THIS TO NOT BE CALLED EVERY FRAME!
            movementModule.SetDestination(target.position);
        }
    }

    /// <summary>
    /// </summary>
    /// <returns>True if the Agent's Target Destination is the Target Position (or close to it)</returns>
    public bool IsFollowingTarget()
    {
        // TODO: CHECK IF CLOSE, NOT EXACTLY THE SAME!

        return movementModule.IsAgentDestination(target.position);
    }

    /// <summary>
    /// </summary>
    /// <returns>True if the Agent has reached it's target</returns>
    public bool ReachedFollowTarget() => movementModule.ReachedTarget(target.position);

    /// <summary>
    /// Removes the follow target
    /// </summary>
    public void StopFollowing() => target = null;

    void Update()
    {
        FollowTarget();
    }
}
