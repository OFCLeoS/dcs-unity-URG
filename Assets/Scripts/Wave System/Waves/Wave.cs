using UnityEngine;

public abstract class Wave {
    protected WaveManager waveManager;

    public Wave(WaveManager waveManager)
    {
        this.waveManager = waveManager;
    }

    abstract protected void InitializeWave();
    abstract public float GetCompletionPercentage();

    protected void CompleteWave()
    {
       Debug.Log("Wave was Completed!");
    }
}