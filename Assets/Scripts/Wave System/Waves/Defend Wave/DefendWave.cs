using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wave of the Defend format
/// </summary>
public class DefendWave : Wave
{
    DefendWaveObjective[] objectives;
    List<DefendWaveObjective> remainingObjectives = new List<DefendWaveObjective>();

    public DefendWave(WaveManager waveManager) : base(waveManager)
    {
        InitializeWave();
    }

    protected override void InitializeWave()
    {
        remainingObjectives.Clear();
        objectives = waveManager.GetDefendWaveObjectsManager.ActivateRandomSet(this);
        remainingObjectives.AddRange(objectives);
        // TODO: PREP PHASE
    }

    /// <summary>
    /// Warn the Wave that an object was destroyed.
    /// </summary>
    /// <param name="destroyedObjective"></param>
    public void ObjectiveDestroyed(DefendWaveObjective destroyedObjective)
    {
        remainingObjectives.Remove(destroyedObjective);
        if (remainingObjectives.Count <= 0)
        {
            CompleteWave();
        }
        // TODO: DESTROY BEHAVIOUR!
    }

    public override float GetCompletionPercentage()
    {
        return remainingObjectives.Count / objectives.Length * 1.0f;
    }

    public override void SetupEnemyForWave(GameObject enemy)
    {
        AIBehaviourGraphManager enemyBehaviourGraphManager = enemy.GetComponent<AIBehaviourGraphManager>();

        // TODO: Chance of "assassins" spawn (Hunt instead of Destroy)
        // We set the Agent's mission to "Destroy", as they will be performing the Destroy part of their Behaviour Graph
        enemyBehaviourGraphManager.SetAgentMission(Mission.Destroy);
        enemyBehaviourGraphManager.SetTarget(remainingObjectives[Random.Range(0, objectives.Length)].transform);
    }
}