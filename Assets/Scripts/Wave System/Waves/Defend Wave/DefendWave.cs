using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wave of the Defend format
/// </summary>
public class DefendWave : Wave
{
    DefendWaveObjective[] objectives;
    List<DefendWaveObjective> remainingObjectives = new List<DefendWaveObjective>();

    public override string WaveObjectiveDescription => "Do not let the Objectives be destroyed";

    public DefendWave(WaveManager waveManager, float waveDuration) : base(waveManager, waveDuration)
    {
    }

    public override void InitializeWave()
    {
        remainingObjectives.Clear();
        objectives = waveManager.GetDefendWaveObjectsManager.ActivateRandomSet(this);
        remainingObjectives.AddRange(objectives);
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
            FinishWave();
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
        AITargetManager targetManager = enemy.GetComponent<AITargetManager>();

        // TODO: Chance of "assassins" spawn (Hunt instead of Destroy)
        if (remainingObjectives.Count > 0)
        {
            // We set the Agent's mission to "Destroy", as they will be performing the Destroy part of their Behaviour Graph
            enemyBehaviourGraphManager.SetAgentMission(Mission.Destroy);
            targetManager.SetTarget(remainingObjectives[Random.Range(0, remainingObjectives.Count)].transform);
        }
        // If no targets are less, the Agents will hunt the player
        else enemyBehaviourGraphManager.SetAgentMission(Mission.Hunt);
    }
}