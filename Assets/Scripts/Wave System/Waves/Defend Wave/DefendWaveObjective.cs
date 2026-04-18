using UnityEngine;

public class DefendWaveObjective : MonoBehaviour
{
    DefendWave wave;

    #region Initialization
    void Awake()
    {
        gameObject.SetActive(false);
    }
    #endregion

    /// <summary>
    /// Activates the Wave Objective for the current wave
    /// </summary>
    public void Activate(DefendWave currentWave)
    {
        wave = currentWave;
        gameObject.SetActive(true);
    }

    // TODO: ONDESTROY COURTESY OF IDAMAGABLE IMPLEMENTATION?


    #region DEBUG
#if UNITY_EDITOR

    [ContextMenu("DEBUG_DESTROY")]
    void DEBUG_DESTROY()
    {
        wave.ObjectiveDestroyed(this);
    }

#endif
    #endregion
}
