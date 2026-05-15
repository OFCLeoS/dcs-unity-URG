using UnityEngine;

public abstract class Wave
{
    protected WaveManager waveManager;

    protected float waveDuration;
    public float WaveDuration { get { return waveDuration; } }

    public Wave(WaveManager waveManager, float waveDuration)
    {
        this.waveManager = waveManager;
        this.waveDuration = waveDuration;
    }

    public abstract void InitializeWave();
    public abstract float GetCompletionPercentage();
    /// <summary>
    /// Sets up an Enemy so that it's objectives align with the wave type.
    /// </summary>
    public abstract void SetupEnemyForWave(GameObject enemy);

    protected virtual void FinishWave() => waveManager.FinishWave();
}