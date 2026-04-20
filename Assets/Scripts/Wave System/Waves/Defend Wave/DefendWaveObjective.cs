using UnityEngine;

public class DefendWaveObjective : DamageableEntity
{
    DefendWave wave;

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
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

    /// <summary>
    /// Deactivates the Wave Objective
    /// </summary>
    public void Deactivate()
    {
        wave = null;
        gameObject.SetActive(false);
    }

    protected override void DestroyEntity()
    {
        wave.ObjectiveDestroyed(this);
        // TODO: Kaboom
        //...
        Deactivate();
    }

    #region DEBUG
#if UNITY_EDITOR

    [ContextMenu("DEBUG_DESTROY")]
    void DEBUG_DESTROY() => DestroyEntity();

#endif
    #endregion
}
