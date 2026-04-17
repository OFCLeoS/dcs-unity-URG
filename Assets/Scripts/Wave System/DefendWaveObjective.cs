using UnityEngine;

public class DefendWaveObjective : MonoBehaviour
{
    DefendWave wave;

    /// <summary>
    /// Activates the Wave Objective for the current wave
    /// </summary>
    public void Activate(DefendWave currentWave)
    {
        wave = currentWave;
        gameObject.SetActive(true);
    }
}
