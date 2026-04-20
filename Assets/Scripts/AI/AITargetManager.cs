using Unity.Behavior;
using Unity.Behavior.GraphFramework;
using UnityEngine;

/// <summary>
/// Module that allows an Agent to manage their current Target
/// </summary>
[RequireComponent(typeof(AIAgent), typeof(AIBehaviourGraphManager))]
public class AITargetManager : MonoBehaviour
{
    AIBehaviourGraphManager behaviourGraphManager;

    bool hasValidTarget = false;

    // For now a Target is valid if it can be damaged, this can be changed if needed
    IDamageable target;

    #region Initialization
    void Awake()
    {
        behaviourGraphManager = GetComponent<AIBehaviourGraphManager>();
    }
    #endregion

    /// <summary>
    /// Makes the Agent no longer have any Target
    /// </summary>
    public void ClearTarget()
    {
        target = null;
        hasValidTarget = false;

        behaviourGraphManager.SetTarget(null);
        behaviourGraphManager.SetValidTarget(false);
    }

    /// <summary>
    /// Sets a new Target for the Agent
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        IDamageable targetDamageable = newTarget.GetComponent<IDamageable>();
        if (targetDamageable != null)
        {
            target = targetDamageable;
            behaviourGraphManager.SetTarget(newTarget);
            hasValidTarget = true;
            behaviourGraphManager.SetValidTarget(hasValidTarget);
        }
        else
        {
            hasValidTarget = false;
            Debug.LogWarning("The target provided to the " + name + " Target Manager does not have an IDamageable implementation, and can therefore not be assigned.");
        }
    }

    void CheckTargetValidity()
    {
        if (target.IsDestroyed) ClearTarget();
    }

    #region Performance
    const float MIN_CHECK_TIME = 0.1f;
    const float MAX_CHECK_TIME = 0.3f;
    float timeSinceLastCheck = 0;
    float nextCheckTime = 0;
    #endregion

    void Update()
    {
        if (!hasValidTarget) return;

        timeSinceLastCheck += Time.deltaTime;
        if (timeSinceLastCheck >= nextCheckTime)
        {
            CheckTargetValidity();
            timeSinceLastCheck = 0;
            nextCheckTime = Random.Range(MIN_CHECK_TIME, MAX_CHECK_TIME); // TODO: Use faster library?
        }
    }
}