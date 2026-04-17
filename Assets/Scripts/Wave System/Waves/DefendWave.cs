using System.Collections.Generic;

public class DefendWave : Wave
{
    HashSet<DefendWaveObjective> objectives;

    public DefendWave(): base()
    {
        InitializeWave();
    }

    protected override void InitializeWave()
    {
        
    }

    public void ObjectiveDestroyed(DefendWaveObjective destroyedObjective)
    {

    }

    public override float GetCompletionPercentage()
    {
        throw new System.NotImplementedException();
    }
}