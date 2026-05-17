using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;

    [SerializeField] DefendWaveObjectsManager defendWaveObjectsManager;
    [SerializeField] EnemySpawningManager enemySpawningManager;

    [SerializeField] MapLayout[] layouts;
    [SerializeField] Transform mapLayoutSpawnPoint;


    public void GenerateMap()
    {
        // TODO: SLOW INSTANTIATION!
        MapLayout generatedLayout = Instantiate(layouts[Random.Range(0, layouts.Length)], mapLayoutSpawnPoint.position, mapLayoutSpawnPoint.rotation);
        List<MapSector> generatedMapSectors = generatedLayout.InitializeLayout();

        List<DefendWaveObjectivesArray> mapObjectiveSets = new List<DefendWaveObjectivesArray>();
        List<EntitySpawner> mapSpawners = new List<EntitySpawner>();
        foreach (MapSector mapSector in generatedMapSectors)
        {
            mapObjectiveSets.AddRange(mapSector.DefendWaveObjectives);
            mapSpawners.AddRange(mapSector.EntitySpawners);
        }
        Debug.Log(mapObjectiveSets.Count);
        defendWaveObjectsManager.InitializNewMapDefendObjectives(mapObjectiveSets);
        enemySpawningManager.InitializNewMapSpawners(mapSpawners);

        waveManager.GenerateNextWave();
    }
}