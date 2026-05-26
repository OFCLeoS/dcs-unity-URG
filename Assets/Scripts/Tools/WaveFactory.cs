using UnityEngine;

public static class WaveFactory
{
    // All in seconds!
    #region Defend Wave
    const float STARTING_DEFEND_WAVE_DURATION = 180;
    const float ENDING_DEFEND_WAVE_DURATION = 900;
    #endregion

    #region Kill Wave
    const float STARTING_KILL_WAVE_DURATION = 900;
    const float ENDING_KILL_WAVE_DURATION = 600;

    const int MINIMUM_KILLS_REQUIRED = 15;
    const int MAXIMUM_KILLS_REQUIRED = 1000;
    #endregion

    /// <summary>
    /// Creates a random wave.
    /// </summary>
    /// <param name="waveManager">The WaveManager that is creating the wave</param>
    /// <returns>The created wave</returns>
    public static Wave CreateRandomWave(WaveManager waveManager)
    {
        // TODO: CHANGE THIS!!!
        int ran = Random.Range(0, 2);
        switch (ran)
        {
            case 0:
                return new KillWave(waveManager,
                Mathf.Lerp(STARTING_KILL_WAVE_DURATION, ENDING_KILL_WAVE_DURATION, waveManager.WaveDifficultyModifier),
                Mathf.RoundToInt(Mathf.Lerp(MINIMUM_KILLS_REQUIRED, MAXIMUM_KILLS_REQUIRED, waveManager.WaveDifficultyModifier)));
            default:
                return new DefendWave(waveManager,
                Mathf.Lerp(STARTING_DEFEND_WAVE_DURATION, ENDING_DEFEND_WAVE_DURATION, waveManager.WaveDifficultyModifier));
        }
    }
}