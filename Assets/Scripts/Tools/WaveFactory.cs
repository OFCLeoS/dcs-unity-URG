using UnityEngine;

public static class WaveFactory {
    
    /// <summary>
    /// Creates a random wave.
    /// </summary>
    /// <param name="waveManager">The WaveManager that is creating the wave</param>
    /// <returns>The created wave</returns>
    public static Wave CreateRandomWave(WaveManager waveManager)
    {
        int ran =Random.Range(0,2);
        switch (ran)
        {
            case 0: return new KillWave(waveManager);
            default: return new KillWave(waveManager); //TODO : CHANGE THIS
        }
    }
}