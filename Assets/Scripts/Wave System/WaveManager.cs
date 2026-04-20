using System;
using UnityEngine;

/// <summary>
/// Is responsible for wave related tasks such as Difficulty Scaling and Wave Creation
/// </summary>
public class WaveManager : MonoBehaviour
{
    int wave;
    Wave currentWave;
    public Wave CurrentWave { get { return currentWave; } }

    float currentWaveTimeLimit;
    float currentWaveTime;

    [SerializeField] EnemySpawningManager enemySpawningManager;
    [SerializeField] DefendWaveObjectsManager defendWaveObjectsManager;

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
        currentWaveTimeLimit = currentWave.GetWaveTimeLimit();
        currentWaveTime = 0;
        enemySpawningManager.Activate(3, currentWave);
    }

    /// <summary>
    /// Event that fires when an enemy was killed
    /// </summary>
    public event Action<AIAgent> OnEnemyKilled;

    public void EnemyKilled(AIAgent enemy)
    {
        OnEnemyKilled.Invoke(enemy);
    }

    void DeactivateAllManagers()
    {
        enemySpawningManager.Deactivate();
        defendWaveObjectsManager.DeactivateAllObjectives();
    }

    public void FinishWave()
    {
        Debug.Log("Wave " + wave + " was Completed!");
        DeactivateAllManagers();
    }

    void Update()
    {
        currentWaveTime += Time.deltaTime;
        if(currentWaveTime >= currentWaveTimeLimit) FinishWave();
    }
}
