using System;
using UnityEngine;

/// <summary>
/// Basis of all AI Agents
/// </summary>
public class AIAgent : DamageableEntity
{
    WaveManager waveManager;
    Wave currentWave;

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    public void Initialize(WaveManager waveManager)
    {
        this.waveManager = waveManager;
        currentWave = waveManager.CurrentWave;
        enabled = true;
    }
    #endregion

    protected override void DestroyEntity()
    {
        waveManager.EnemyKilled(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// Updates the Agent's Current Objective in relation to the current wave
    /// </summary>
    public void UpdateAgentObjective() => currentWave.SetupEnemyForWave(gameObject);


    #region DEBUG
#if UNITY_EDITOR

    [ContextMenu("DEBUG_DESTROY")]
    void KILL() => DestroyEntity();

#endif
    #endregion
}