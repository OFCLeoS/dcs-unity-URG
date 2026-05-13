using System.Collections.Generic;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    List<MapRandomSector> randomMapSectors = new List<MapRandomSector>();

    #region Initialization
    public void InitializeLayout()
    {
        foreach (MapRandomSector randomSector in randomMapSectors)
        {
            randomSector.Initialize();
        }
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
