using UnityEngine;
using UnityEngine.AI;

public class HumanoidAIAgentAnimationController : HumanoidAnimationController
{
    [SerializeField] NavMeshAgent navMeshAgent;

    #region Initialization
    void Awake()
    {
        if (!navMeshAgent)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        float agentSpeed = navMeshAgent.speed;
        Initalize(agentSpeed, agentSpeed);
    }
    #endregion

    void UpdateAgentMovementBlendTree()
    {
        if (navMeshAgent.velocity != Vector3.zero)
        {
            animator.SetBool(isMovingBool, true);

            // TODO: CAN MAKE THIS MORE PERFORMANT?
            Vector3 localAgentVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            animator.SetFloat(xMovementFloat, localAgentVelocity.x / _maxXSpeed);
            animator.SetFloat(yMovementFloat, localAgentVelocity.z / _maxZSpeed);
        }
        else animator.SetBool(isMovingBool, false);
    }

    void Update()
    {
        UpdateAgentMovementBlendTree();
    }
}