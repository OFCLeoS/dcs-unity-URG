using System.Collections.Generic;

public class DefendWave : Wave
{
    HashSet<DefendWaveObjective> objectives;
    int destroyedObjectives = 0;

    public DefendWave(WaveManager waveManager): base(waveManager)
    {
        InitializeWave();
    }

    protected override void InitializeWave()
    {
        destroyedObjectives = 0;
        objectives = new HashSet<DefendWaveObjective>(waveManager.GetDefendWaveObjectsManager.ActivateRandomSet(this));
        // TODO: PREP PHASE
    }

    /// <summary>
    /// Warn the Wave that an object was destroyed.
    /// </summary>
    /// <param name="destroyedObjective"></param>
    public void ObjectiveDestroyed(DefendWaveObjective destroyedObjective)
    {
        destroyedObjectives++;
        if(destroyedObjectives == objectives.Count){
            CompleteWave();
        }
        // TODO: DESTROY BEHAVIOUR!
    }

    public override float GetCompletionPercentage()
    {
        return destroyedObjectives/objectives.Count*1.0f;
    }
}