using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generator Responsible for generating the map and then placing the player in the map, as well as destroying the map and bringing the player back to the HUB.
/// </summary>
public class MapGenerator : MonoBehaviour
{
    // TODO: THIS IS VERY BAD CODE, DO BETTER SEPERATION OF CONCERNS!!! 

    [SerializeField] WaveManager waveManager;

    [SerializeField] DefendWaveObjectsManager defendWaveObjectsManager;
    [SerializeField] EnemySpawningManager enemySpawningManager;

    [SerializeField] Elevator elevator;
    [SerializeField] Vector3 hubPosition;
    [Tooltip("The position the player will be in once the wave starts")]
    [SerializeField] Vector3 wavePosition;

    [SerializeField] MapLayout[] layouts;
    [SerializeField] Transform mapLayoutSpawnPoint;

    bool isGenerating = false;
    bool playerOnTheMap = false;

    [SerializeField] ColliderActivator waveMapElevatorActivator;
    [SerializeField] ElevatorDoorsController waveMapElevatorController;

    [SerializeField] ElevatorDoorsController hubMapElevatorController;

    [SerializeField] QuizComputer quizComputer;

    [Tooltip("The minimum time the player will spend on the elevator")]
    [SerializeField] float _minimumElevatorTime = 5;
    float minimumTimeLeftToBeInElevator = 0;

    bool goingToHUB = false;

    void Awake()
    {
        enabled = false;
    }

    public void StartMapGeneration(Player player)
    {
        goingToHUB = false;
        elevator.StartElevatorSequence(player, false);

        generatedLayout = Instantiate(layouts[Random.Range(0, layouts.Length)], mapLayoutSpawnPoint.position, mapLayoutSpawnPoint.rotation);

        generatedLayout.StartLayoutInitialization();

        generatedMapSectors.Clear();
        generatedMapObjectiveSets.Clear();
        generatedMapSpawners.Clear();

        minimumTimeLeftToBeInElevator = _minimumElevatorTime;

        // Player can only go back to HUB once wave is done
        waveMapElevatorActivator.DisableActivator();

        isGenerating = true;
        playerOnTheMap = false;
        enabled = true;
    }

    // We will be generating the map a little each frame, as to avoid a lag spike.
    MapLayout generatedLayout;
    List<MapSector> generatedMapSectors = new List<MapSector>();
    List<DefendWaveObjectivesArray> generatedMapObjectiveSets = new List<DefendWaveObjectivesArray>();
    List<EntitySpawner> generatedMapSpawners = new List<EntitySpawner>();
    void MapGenStep()
    {
        MapSector newGeneratedSector = generatedLayout.LayoutInitalizationStep();
        if (newGeneratedSector != null)
        {
            generatedMapSectors.Add(newGeneratedSector);
            if (newGeneratedSector.DefendWaveObjectives.Length > 0)
            {
                generatedMapObjectiveSets.AddRange(newGeneratedSector.DefendWaveObjectives);
            }
            if (newGeneratedSector.EntitySpawners.Length > 0)
            {
                generatedMapSpawners.AddRange(newGeneratedSector.EntitySpawners);
            }
        }
        else
        {
            defendWaveObjectsManager.InitializNewMapDefendObjectives(generatedMapObjectiveSets);
            enemySpawningManager.InitializNewMapSpawners(generatedMapSpawners);

            waveManager.GenerateNextWave();

            isGenerating = false;
        }
    }

    public void StartMapDestruction(Player player)
    {
        goingToHUB = true;
        elevator.StartElevatorSequence(player, true);

        minimumTimeLeftToBeInElevator = _minimumElevatorTime;

        playerOnTheMap = false;
        DestroyMap();

        enabled = true;
    }

    void DestroyMap()
    {
        Destroy(generatedLayout.gameObject);
    }

    void HandleMapGen()
    {
        if (!isGenerating)
        {
            if (!playerOnTheMap)
            {
                minimumTimeLeftToBeInElevator -= Time.deltaTime;
                if (minimumTimeLeftToBeInElevator <= 0)
                {
                    waveManager.StartPreparationPhase();
                    elevator.StopElevatorSequence(wavePosition);
                    waveMapElevatorController.OpenDoorSequence();

                    playerOnTheMap = true;
                    enabled = false;
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("The Map Generator was activated, but no generating was in progress!");
#endif
                enabled = false;
                return;
            }
        }
        else MapGenStep();
    }

    void HandleGotoHUB()
    {
        if (!playerOnTheMap)
        {
            minimumTimeLeftToBeInElevator -= Time.deltaTime;
            if (minimumTimeLeftToBeInElevator <= 0)
            {
                elevator.StopElevatorSequence(hubPosition);
                hubMapElevatorController.OpenDoorSequence();
                quizComputer.EnableQuizComputer();

                playerOnTheMap = true;
                enabled = false;
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("The Map Generator was activated, but no generating was in progress!");
#endif
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (!goingToHUB) HandleMapGen();
        else HandleGotoHUB();
    }
}