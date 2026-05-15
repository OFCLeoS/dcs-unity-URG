using System;
using UnityEngine;

/// <summary>
/// Basis of all AI Agents
/// </summary>
public class AIAgent : DamageableEntity
{
    [SerializeField] AIAgentAttributes agentAttributes;
    [SerializeField] AIMovementModule movementModule;
    [SerializeField] AttackController attackController;

    WaveManager waveManager;
    Wave currentWave;

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
        if (!movementModule)
        {
            movementModule = GetComponent<AIMovementModule>();
        }
        if (!movementModule)
        {
            movementModule = GetComponent<AIMovementModule>();
        }
        enabled = false;
    }

    public void Initialize(WaveManager waveManager)
    {
        this.waveManager = waveManager;
        currentWave = waveManager.CurrentWave;
        AdaptAttributesToWaveDifficulty(waveManager.WaveDifficultyModifier);
        enabled = true;
    }
    void AdaptAttributesToWaveDifficulty(float waveDifficultyModifier)
    {
        currentHealth = Mathf.Lerp(agentAttributes.minAgentHealth, agentAttributes.maxAgentHealth, waveDifficultyModifier);
        movementModule.SetMovementSpeed(Mathf.Lerp(agentAttributes.minMovementSpeed, agentAttributes.maxMovementSpeed, waveDifficultyModifier));
        attackController.GetSelectedWeapon().SetDamage(
            Mathf.LerpUnclamped(agentAttributes.minDamage, agentAttributes.maxDamage, waveDifficultyModifier)
        ); // Funny Bug will happen due to unclamp :D (Only on ridiculous high waves)
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