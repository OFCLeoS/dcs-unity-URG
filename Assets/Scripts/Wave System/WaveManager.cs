using System;
using UnityEngine;

/// <summary>
/// Is responsible for wave related tasks such as Difficulty Scaling and Wave Creation. Should only be active during waves.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawningManager enemySpawningManager;

    [SerializeField] DefendWaveObjectsManager defendWaveObjectsManager;

    [SerializeField] private PlayerUIBehaviour playerUIBehaviour;
    public DefendWaveObjectsManager GetDefendWaveObjectsManager => defendWaveObjectsManager;

    [Tooltip("Quiz Manager reference in order to get tip level.")]
    [SerializeField] QuizManager quizManager;

    [Tooltip("Wave that has the highest difficulty. (Anything above this is for bragging rights only)")]
    [SerializeField] int maxDifficultyWave = 50;

    public float WaveDifficultyModifier { get; private set; }

    [SerializeField] int waveNumber;
    Wave currentWave;
    public Wave CurrentWave { get { return currentWave; } }

    [SerializeField] float preperationTime = 20;
    float preperationTimeLeft;
    bool inPreperationPhase = true;

    float timeLeftForCurrentWave;

    [SerializeField] ColliderActivator waveMapElevatorActivator;


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
        playerUIBehaviour.ChangeWaveNumber(waveNumber);
        SetWaveDifficultyModifier();
        currentWave = WaveFactory.CreateRandomWave(this);
        currentWave.InitializeWave();
        playerUIBehaviour.SetTimerColour(true);
        playerUIBehaviour.SetObjectiveText(currentWave.WaveObjectiveDescription);
    }

    public void StartPreparationPhase()
    {
        inPreperationPhase = true;
        // TODO: MAKE THIS DYNAMIC?
        preperationTimeLeft = preperationTime;
        enabled = true;
    }

    void StartWave()
    {
        inPreperationPhase = false;
        timeLeftForCurrentWave = currentWave.WaveDuration;
        playerUIBehaviour.ChangeWaveTimer(timeLeftForCurrentWave);
        enemySpawningManager.Activate(currentWave);
        playerUIBehaviour.SetTimerColour(false);
    }

    /// <summary>
    /// Event that fires when an enemy was killed
    /// </summary>
    public event Action<AIAgent> OnEnemyKilled;

    public void EnemyKilled(AIAgent enemy)
    {
        OnEnemyKilled?.Invoke(enemy);
        // We check for enabled in case a last enemy is killed and the HUD is not properly updated
        if (enabled)
        {
            playerUIBehaviour.SetObjectiveText(currentWave.WaveObjectiveDescription);
        }
    }

    void DeactivateAllManagers()
    {
        enemySpawningManager.Deactivate();
        defendWaveObjectsManager.DeactivateAllObjectives();
    }

    public void FinishWave()
    {
        // DO SOMETHING WITH THIS
        float waveCompletionPercentage = currentWave.GetCompletionPercentage();
        if (waveCompletionPercentage >= (3.0f / 3.0f))
        {
            quizManager.SetHintLevel(HintLevel.PARAGRAPH);
        }
        else if (waveCompletionPercentage >= (2.0f / 3.0f))
        {
            quizManager.SetHintLevel(HintLevel.SUB_TOPIC);
        }
        else if (waveCompletionPercentage >= (1.0f / 3.0f))
        {
            quizManager.SetHintLevel(HintLevel.TOPIC);
        }
        else
        {
            quizManager.SetHintLevel(HintLevel.NO_HINT);
        }
        Debug.Log("Wave " + waveNumber + " was Completed with a completion percentage of " + waveCompletionPercentage);
        DeactivateAllManagers();

        // Player go back to HUB once wave is done
        waveMapElevatorActivator.EnableActivator();

        enabled = false;
        playerUIBehaviour.SetObjectiveText("Complete the next quiz");
        playerUIBehaviour.ChangeWaveTimer(0);
    }

    void HandleWave()
    {
        if (inPreperationPhase)
        {
            playerUIBehaviour.ChangeWaveTimer(preperationTimeLeft);
            preperationTimeLeft -= Time.deltaTime;
            if (preperationTimeLeft <= 0) StartWave();
        }
        else
        {
            playerUIBehaviour.ChangeWaveTimer(timeLeftForCurrentWave);
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
        enabled = true;
        StartWave();
    }
    [ContextMenu("Force Generate +10 Wave")]
    public void DEBUG_FORCE_GENERATE_P5WAVE()
    {
        waveNumber += 10;
        GenerateNextWave();
        enabled = true;
        StartWave();
    }
    [ContextMenu("Force Start Wave")]
    public void DEBUG_FORCE_START_WAVE()
    {
        StartWave();
    }
    [ContextMenu("Force Finish Wave")]
    public void DEBUG_FORCE_FINISH_WAVE()
    {
        FinishWave();
    }
#endif
    #endregion
}
