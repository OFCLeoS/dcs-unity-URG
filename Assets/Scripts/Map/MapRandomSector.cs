using System.Collections.Generic;
using UnityEngine;

public class MapRandomSector : MonoBehaviour
{
    [SerializeField] Vector2 dimensions;
    [SerializeField] DoorPoint[] doorPoints;
    [Tooltip("Which sectors are able to spawn here")]
    [SerializeField] MapSector[] possibleSectors;

    #region Properties
    public Vector2 Dimensions { get { return dimensions; } }
    #endregion

    #region Initialization
    /// <summary>
    /// Selects a random Sector to spawn
    /// </summary>
    public void Initialize()
    {
        MapSector chosenSector = Instantiate(possibleSectors[Random.Range(0, possibleSectors.Length)], transform.position, transform.rotation);
    }
    #endregion


    public void SetActiveDoorPoints(List<Vector2> doorPointsToActivate)
    {
        foreach (DoorPoint doorPoint in doorPoints)
        {
            bool foundDoorPoint = false;
            foreach (Vector2 doorPointDeltas in doorPointsToActivate)
            {
                if (doorPoint.dXdY == doorPointDeltas)
                {
                    foundDoorPoint = true;
                    doorPointsToActivate.Remove(doorPointDeltas);
                    break;
                }
            }
            if (foundDoorPoint)
            {
                doorPoint.wall.SetActive(false);
                doorPoint.doorway.SetActive(true);
            }
            else
            {
                doorPoint.wall.SetActive(true);
                doorPoint.doorway.SetActive(false);
            }
        }
        // If there are still points here, they don't exist on this Random Sector
        foreach (Vector2 doorPointDeltas in doorPointsToActivate)
        {
            Debug.LogWarning($"The Door Point of deltas: {doorPointDeltas} does not exist on the {transform.name} Random Sector!");
        }
    }
}