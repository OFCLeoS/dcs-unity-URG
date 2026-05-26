using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wave of the Defend format
/// </summary>
public class KillWave : Wave
{
    readonly int _requiredKills = 50;
    int kills = 0;

    public override string WaveObjectiveDescription => $"Kill {_requiredKills} robots ({_requiredKills - kills} Left)";

    public KillWave(WaveManager waveManager, float waveDuration, int requiredKills) : base(waveManager, waveDuration)
    {
        _requiredKills = requiredKills;
        waveManager.OnEnemyKilled += EnemyKilled;
    }

    public override void InitializeWave()
    {

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
        return (kills * 1.0f) / (_requiredKills * 1.0f);
    }

    public override void SetupEnemyForWave(GameObject enemy)
    {
        AIBehaviourGraphManager enemyBehaviourGraphManager = enemy.GetComponent<AIBehaviourGraphManager>();
        enemyBehaviourGraphManager.SetAgentMission(Mission.Hunt);
    }
}