using System.Collections.Generic;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    List<MapRandomSector> mapSections = new List<MapRandomSector>();

    public void AddMapSection(MapRandomSector randomSector)
    {
        mapSections.Add(randomSector);
    }
}
