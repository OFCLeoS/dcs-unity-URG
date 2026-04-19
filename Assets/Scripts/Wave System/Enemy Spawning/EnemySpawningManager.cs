using UnityEngine;

/// <summary>
/// Manager that is responsible for spawning enemies throughout a wave.
/// </summary>
public class EnemySpawningManager : MonoBehaviour
{
    const float MIN_TIME_BETWEEN_SPAWNS = 1;

    float timeBetweenSpawns = 3;
    float timeSinceLastSpawn = 0;

    bool isActive = false;

    Wave currentWave;

    [SerializeField] EntitySpawner[] enemySpawners;

    [SerializeField] GameObject[] enemies;

    [SerializeField] Transform player;

    public void SetTimeBetweenSpawns(float time)
    {
        if (time <= MIN_TIME_BETWEEN_SPAWNS) time = MIN_TIME_BETWEEN_SPAWNS;

        timeBetweenSpawns = time;
    }

    public void Activate(float timeBetweenSpawns, Wave currentWave)
    {
        SetTimeBetweenSpawns(timeBetweenSpawns);
        Activate(currentWave);
    }

    public void Activate(Wave currentWave)
    {
        this.currentWave = currentWave;
        timeSinceLastSpawn = 0;
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        currentWave = null;
    }

    /// <summary>
    /// Randomly spawn an enemy in a random spawner
    /// </summary>
    void RandomSpawn()
    {
        GameObject spawnedEnemy = enemySpawners[Random.Range(0, enemySpawners.Length)].SpawnEntity(enemies[Random.Range(0, enemies.Length)]);
        spawnedEnemy.GetComponent<AIBootstrapper>().Initialize(player);
        currentWave.SetupEnemyForWave(spawnedEnemy);
    }

    void Update()
    {
        if (isActive)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= timeBetweenSpawns)
            {
                RandomSpawn();
                timeSinceLastSpawn = 0;
            }
        }
    }
}
