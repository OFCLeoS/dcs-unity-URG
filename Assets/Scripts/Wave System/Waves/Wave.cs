using UnityEngine;

public abstract class Wave {
    protected WaveManager waveManager;

    public Wave(WaveManager waveManager)
    {
        this.waveManager = waveManager;
    }

    abstract protected void InitializeWave();
    abstract public float GetCompletionPercentage();
    /// <summary>
    /// Sets up an Enemy so that it's objectives align with the wave type.
    /// </summary>
    abstract public void SetupEnemyForWave(GameObject enemy);

    protected void CompleteWave() => waveManager.FinishWave();
}