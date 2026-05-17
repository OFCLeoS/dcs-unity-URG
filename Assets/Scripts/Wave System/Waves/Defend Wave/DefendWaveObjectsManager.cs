using System.Collections.Generic;
using UnityEngine;

public class DefendWaveObjectsManager : MonoBehaviour
{
    [Tooltip("Each set represents the objectives a player would have to defend in a single wave.")]
    [SerializeField] List<DefendWaveObjectivesArray> objectiveSets;

    public void InitializNewMapDefendObjectives(List<DefendWaveObjectivesArray> newObjectiveSets)
    {
        objectiveSets.Clear();
        objectiveSets.AddRange(newObjectiveSets);
    }

    /// <summary>
    /// Activates a random set of objectives from the objective sets, and sets their wave to be the current one
    /// </summary>
    public DefendWaveObjective[] ActivateRandomSet(DefendWave currentWave)
    {
        DefendWaveObjective[] chosenObjectives = objectiveSets[Random.Range(0, objectiveSets.Count)].objectives;
        // We activate a random set of objectives
        foreach (DefendWaveObjective objective in chosenObjectives)
        {
            objective.Activate(currentWave);
        }
        return chosenObjectives;
    }

    public void DeactivateAllObjectives()
    {
        foreach (DefendWaveObjectivesArray objectiveArray in objectiveSets)
        {
            foreach (DefendWaveObjective objective in objectiveArray.objectives)
            {
                objective.Deactivate();
            }
        }
    }

    //TODO: RESET AFTER EACH WAVE?
}