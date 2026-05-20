using UnityEngine;

public class MapSector : MonoBehaviour
{
    [SerializeField] GameObject frameworkParent;
    [SerializeField] DoorwayOverlapRemover[] doorwayOverlapRemovers;

    [Header("Wave Settings")]
    [SerializeField] DefendWaveObjectivesArray[] defendWaveObjectives;
    [SerializeField] EntitySpawner[] entitySpawners;

    public DefendWaveObjectivesArray[] DefendWaveObjectives { get { return defendWaveObjectives; } }
    public EntitySpawner[] EntitySpawners { get { return entitySpawners; } }

    #region Initialization
    public void Initialize()
    {
        for (int i = 0; i < doorwayOverlapRemovers.Length; i++)
        {
            doorwayOverlapRemovers[i].HandleOverlap();
        }
        Destroy(frameworkParent);
    }
    #endregion
}