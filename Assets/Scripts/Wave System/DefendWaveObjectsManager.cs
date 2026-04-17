using UnityEngine;

public class DefendWaveObjectsManager : MonoBehaviour
{
    [Tooltip("Each set represents the objectives a player would have to defend in a single wave.")]
    [SerializeField] DefendWaveObjective[][] objectiveSets;
    
    /// <summary>
    /// Activates a random set of objectives from the objective sets, and sets their wave to be the current one
    /// </summary>
    public void ActivateRandomSet(DefendWave currentWave)
    {
        // We activate a random set of objectives
        foreach (DefendWaveObjective objective in objectiveSets[Random.Range(0,objectiveSets.Length)])
        {
            objective.Activate(currentWave);
        }
    }
}