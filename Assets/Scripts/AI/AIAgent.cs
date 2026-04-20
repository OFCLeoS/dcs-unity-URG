using UnityEngine;

/// <summary>
/// Basis of all AI Agents
/// </summary>
public class AIAgent : MonoBehaviour{

    Wave currentWave;

    #region Initialization
    void Awake()
    {
        enabled = false;
    }

    public void Initialize(Wave currentWave)
    {
        this.currentWave = currentWave;
        enabled = true;
    }
    #endregion

    /// <summary>
    /// Updates the Agent's Current Objective in relation to the current wave
    /// </summary>
    public void UpdateAgentObjective() => currentWave.SetupEnemyForWave(gameObject);
}