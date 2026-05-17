using UnityEngine;

public class MapSector : MonoBehaviour
{
    [SerializeField] DefendWaveObjectivesArray[] defendWaveObjectives;
    [SerializeField] EntitySpawner[] entitySpawners;

    public DefendWaveObjectivesArray[] DefendWaveObjectives { get { return defendWaveObjectives; } }
    public EntitySpawner[] EntitySpawners { get { return entitySpawners; } }
}