using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager that is responsible for spawning enemies throughout a wave.
/// </summary>
public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] float maximumTimeBetweenSpawns = 7;
    [SerializeField] float minimumTimeBetweenSpawns = 1;

    float timeBetweenSpawns = 1;
    float timeUntilNextSpawn = 1;

    bool isActive = false;

    [SerializeField] WaveManager waveManager;

    [SerializeField] List<EntitySpawner> enemySpawners;

    [SerializeField] GameObject[] enemies;

    [SerializeField] Transform player;

    Wave currentWave;

    [SerializeField] List<GameObject> enemiesInWave = new List<GameObject>();

    #region Initialization
    void Awake()
    {
        if (!waveManager)
        {
            waveManager = GetComponent<WaveManager>();
            if (!waveManager)
            {
                Debug.LogError(name + "'s Wave Manager was not found. Enemy spawning will not function!");
            }
        }
    }

    public void InitializNewMapSpawners(List<EntitySpawner> newEntitySpawners)
    {
        enemySpawners.Clear();
        enemySpawners.AddRange(newEntitySpawners);
    }
    #endregion

    public void Activate(Wave currentWave)
    {
        this.currentWave = currentWave;
        timeBetweenSpawns = Mathf.Lerp(maximumTimeBetweenSpawns, minimumTimeBetweenSpawns, waveManager.WaveDifficultyModifier);

        timeUntilNextSpawn = timeBetweenSpawns;
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        currentWave = null;
    }

    public void DestroyAllWaveEnemies()
    {
        for (int i = 0; i < enemiesInWave.Count; i++)
        {
            if (enemiesInWave[i] != null)
            {
                Destroy(enemiesInWave[i]);
            }
        }
    }

    /// <summary>
    /// Randomly spawn an enemy in a random spawner
    /// </summary>
    void RandomSpawn()
    {
        GameObject spawnedEnemy = enemySpawners[Random.Range(0, enemySpawners.Count)].SpawnEntity(enemies[Random.Range(0, enemies.Length)]);
        spawnedEnemy.GetComponent<AIBootstrapper>().Initialize(player, waveManager);
        currentWave.SetupEnemyForWave(spawnedEnemy);
        enemiesInWave.Add(spawnedEnemy);
    }

    void Update()
    {
        if (isActive)
        {
            timeUntilNextSpawn -= Time.deltaTime;
            if (timeUntilNextSpawn <= 0)
            {
                RandomSpawn();
                timeUntilNextSpawn = timeBetweenSpawns;
            }
        }
    }
}
