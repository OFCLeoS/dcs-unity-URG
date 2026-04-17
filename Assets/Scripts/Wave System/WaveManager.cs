using UnityEngine;

public class WaveManager : MonoBehaviour
{
    int wave;
    Wave currentWave;
    [SerializeField] DefendWaveObjectsManager defendWaveObjectsManager;

    #region Initialization
    void Awake()
    {
        if (!defendWaveObjectsManager)
        {
            Debug.LogError("The Defend Wave Objects Manager was not found in the \"" + name + "\" Wave Manager. It will not function properly.");
            enabled = false;
        }
    }
    void Start()
    {
        // TODO: THIS IS TEMPORARY!
        StartNextWave();
    }
    #endregion

    public void StartNextWave()
    {
        wave++;
        currentWave = WaveFactory.CreateRandomWave();
        // TODO: CHOOSE WAVE TYPE
    }

    public delegate void OnEnemyKilled();
}
