using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    [SerializeField] List<MapRandomSector> randomMapSectors = new List<MapRandomSector>();
    [SerializeField] NavMeshSurface navMeshSurface;

    #region Initialization
    public void StartLayoutInitialization()
    {
        currentGeneratedSectorIndex = 0;
    }
    #endregion


    int currentGeneratedSectorIndex = 0;
    /// <summary>
    /// Initializes the next Map sector in this layout and returns it
    /// </summary>
    /// <returns>Null if not Sectors are left</returns>
    public MapSector LayoutInitalizationStep()
    {
        if (currentGeneratedSectorIndex >= randomMapSectors.Count)
        {
            navMeshSurface.BuildNavMesh();
            return null;   
        }

        MapSector newGeneratedMapSector = randomMapSectors[currentGeneratedSectorIndex].Initialize();
        currentGeneratedSectorIndex++;
        return newGeneratedMapSector;
    }


    #region Draw.io Translator Related
    /// <summary>
    /// Used for layout generator with the draw.io translator
    /// </summary>
    /// <param name="randomSector"></param>
    public void AddMapSection(MapRandomSector randomSector)
    {
        randomMapSectors.Add(randomSector);
    }
    #endregion
}
