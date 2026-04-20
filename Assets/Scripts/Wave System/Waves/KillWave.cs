using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wave of the Defend format
/// </summary>
public class KillWave : Wave
{
    readonly int _requiredKills = 5;
    int kills = 0;

    public KillWave(WaveManager waveManager) : base(waveManager)
    {
        waveManager.OnEnemyKilled += EnemyKilled;
        InitializeWave();
    }

    protected override void InitializeWave()
    {
        // TODO: PREP PHASE?
    }


    public override float GetWaveTimeLimit()
    {
        // TODO: CHANGE THIS FOR DIFFICULTY SCALING
        // EQUATION: (log10(x+1))/1.23
        return 600;
    }

    public void EnemyKilled(AIAgent enemy)
    {
        kills++;
        if (kills >= _requiredKills)
        {
            FinishWave();
        }
    }

    protected override void FinishWave()
    {
        waveManager.OnEnemyKilled -= EnemyKilled;
        base.FinishWave();
    }


    public override float GetCompletionPercentage()
    {
        return kills / _requiredKills * 1.0f;
    }

    public override void SetupEnemyForWave(GameObject enemy)
    {
        AIBehaviourGraphManager enemyBehaviourGraphManager = enemy.GetComponent<AIBehaviourGraphManager>();
        enemyBehaviourGraphManager.SetAgentMission(Mission.Hunt);
    }
}