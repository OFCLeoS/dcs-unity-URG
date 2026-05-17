using System;
using UnityEngine;

/// <summary>
/// Is responsible for wave related tasks such as Difficulty Scaling and Wave Creation. Should only be active during waves.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawningManager enemySpawningManager;

    [SerializeField] DefendWaveObjectsManager defendWaveObjectsManager;
    public DefendWaveObjectsManager GetDefendWaveObjectsManager => defendWaveObjectsManager;

    [Tooltip("Quiz Manager reference in order to get tip level.")]
    [SerializeField] QuizManager quizManager;

    [Tooltip("Wave that has the highest difficulty. (Anything above this is for bragging rights only)")]
    [SerializeField] int maxDifficultyWave = 50;

    public float WaveDifficultyModifier { get; private set; }

    int waveNumber;
    Wave currentWave;
    public Wave CurrentWave { get { return currentWave; } }

    float preperationTimeLeft;
    bool inPreperationPhase = true;

    float timeLeftForCurrentWave;


    #region Initialization
    void Awake()
    {
        CheckComponentsExistence();
        enabled = false;
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
    #endregion

    void SetWaveDifficultyModifier()
    {
        float x = waveNumber * 1.0f / maxDifficultyWave * 1.0f;
        WaveDifficultyModifier = Mathf.Pow(
            (Mathf.Exp(x / 2 * 1.0f) - 1.0f)
            /
            (Mathf.Exp(1 * 1.0f / 2 * 1.0f) - 1.0f)
            , 1.5f);
    }

    public void GenerateNextWave()
    {
        waveNumber++;
        SetWaveDifficultyModifier();
        currentWave = WaveFactory.CreateRandomWave(this);
        currentWave.InitializeWave();
        StartPreparationPhase();
    }

    void StartPreparationPhase()
    {
        inPreperationPhase = true;
        // TODO: MAKE THIS DYNAMIC?
        preperationTimeLeft = 60;
        enabled = true;
    }

    void StartWave()
    {
        inPreperationPhase = false;
        timeLeftForCurrentWave = currentWave.WaveDuration;
        enemySpawningManager.Activate(currentWave);
    }

    /// <summary>
    /// Event that fires when an enemy was killed
    /// </summary>
    public event Action<AIAgent> OnEnemyKilled;

    public void EnemyKilled(AIAgent enemy)
    {
        OnEnemyKilled?.Invoke(enemy);
    }

    void DeactivateAllManagers()
    {
        enemySpawningManager.Deactivate();
        defendWaveObjectsManager.DeactivateAllObjectives();
    }

    public void FinishWave()
    {
        // DO SOMETHING WITH THIS
        float waveCompletionPercenrtage = currentWave.GetCompletionPercentage();
        if (waveCompletionPercenrtage >= (3.0f / 3.0f))
        {
            quizManager.SetHintLevel(HintLevel.PARAGRAPH);
        }
        else if (waveCompletionPercenrtage >= (2.0f / 3.0f))
        {
            quizManager.SetHintLevel(HintLevel.SUB_TOPIC);
        }
        else if (waveCompletionPercenrtage >= (1.0f / 3.0f))
        {
            quizManager.SetHintLevel(HintLevel.TOPIC);
        }
        else
        {
            quizManager.SetHintLevel(HintLevel.NO_HINT);
        }
        Debug.Log("Wave " + waveNumber + " was Completed!");
        DeactivateAllManagers();
        enabled = false;
    }

    void HandleWave()
    {
        if (inPreperationPhase)
        {
            preperationTimeLeft -= Time.deltaTime;
            if (preperationTimeLeft <= 0) StartWave();
        }
        else
        {
            timeLeftForCurrentWave -= Time.deltaTime;
            if (timeLeftForCurrentWave <= 0) FinishWave();
        }
    }

    void Update() => HandleWave();

    #region DEBUGGING
#if UNITY_EDITOR
    [ContextMenu("Force Generate Wave")]
    public void DEBUG_FORCE_GENERATE_WAVE()
    {
        GenerateNextWave();
        StartWave();
    }
    [ContextMenu("Force Start Wave")]
    public void DEBUG_FORCE_START_WAVE()
    {
        StartWave();
    }
#endif
    #endregion
}
