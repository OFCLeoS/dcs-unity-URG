using UnityEngine;

public class WaveManager : MonoBehaviour
{
    int wave;
    Wave currentWave;
    [SerializeField] DefendWaveObjectsManager defendWaveObjectsManager;
    [SerializeField] EnemySpawningManager enemySpawningManager;

    public DefendWaveObjectsManager GetDefendWaveObjectsManager => defendWaveObjectsManager;

    #region Initialization
    void Awake()
    {
        CheckComponentsExistence();
    }

    void CheckComponentsExistence()
    {
        if (!defendWaveObjectsManager)
        {
            defendWaveObjectsManager = GetComponent<DefendWaveObjectsManager>();
            if (!defendWaveObjectsManager)
            {
                Debug.LogError("The Defend Wave Objects Manager was not found in the \"" + name + "\" Wave Manager. The Wave Manager will not function.");
                enabled = false;
            }
        }
        if (!enemySpawningManager)
        {
            enemySpawningManager = GetComponent<EnemySpawningManager>();
            if (!enemySpawningManager)
            {
                Debug.LogError("The Enemy Spawning Manager was not found in the \"" + name + "\" Wave Manager. The Wave Manager will not function.");
                enabled = false;
            }
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
        currentWave = WaveFactory.CreateRandomWave(this);
        enemySpawningManager.Activate(3,currentWave);
    }

    public delegate void OnEnemyKilled();

    public void FinishWave()
    {
        Debug.Log("Wave " + wave + " was Completed!");
    }
}
