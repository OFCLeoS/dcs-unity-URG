using System.Collections.Generic;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    [SerializeField] List<MapRandomSector> randomMapSectors = new List<MapRandomSector>();

    #region Initialization
    public List<MapSector> InitializeLayout()
    {
        List<MapSector> generatedMapSectors = new List<MapSector>();
        foreach (MapRandomSector randomSector in randomMapSectors)
        {
            MapSector newGeneratedMapSector = randomSector.Initialize();
            if (newGeneratedMapSector != null)
            {
                generatedMapSectors.Add(newGeneratedMapSector);
            }
        }
        return generatedMapSectors;
    }
    #endregion


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
