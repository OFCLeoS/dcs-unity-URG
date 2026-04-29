using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a Random Map
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GeneratedRoom> roomsToGenerate;
    List<Room> rooms;

    [Tooltip("How big is a cell compared to 1 Unity Unit. (E.g. Cell Size = 5 means that 1 cell is equivalent to 5 Unity Units)")]
    [SerializeField] int cellSize;

    /// <summary>
    /// How far from 0,0 can rooms spawn at
    /// </summary>
    [SerializeField] Vector2Int spawnJitter;

    Map currentFloorMap;

    #region Initialization
    void Awake()
    {
        if (cellSize <= 0)
        {
#if UNITY_EDITOR
            Debug.LogError("The Map Generator has an invalid cell size, it will not work!");
#endif
            gameObject.SetActive(false);
        }
    }
    #endregion

    void CreateNewFloor()
    {
        if (!SpawnRooms())
        {
#if UNITY_EDITOR
            Debug.LogError("The Map Generator encountered an error when creating the rooms array!");
#endif
            gameObject.SetActive(false);
        }
        currentFloorMap = new Map(rooms);
        ResolveRoomCollisions();
    }

    /// <summary>
    /// Adds a representation of all the rooms to be spawned to the rooms array.
    /// </summary>
    /// <returns>True if successful</returns>
    bool SpawnRooms()
    {
        if (roomsToGenerate.Count <= 0) return false;
        foreach (GeneratedRoom room in roomsToGenerate)
        {
            rooms.Add(room.GetRoomRepresentation(cellSize));
        }
        return true;
    }

    /// <summary>
    /// Pushes rooms away from each other using their Displacement Vectors until no collisions are detected
    /// </summary>
    void ResolveRoomCollisions()
    {

    }

    #region DEBUGGING
#if UNITY_EDITOR
    [ContextMenu("DEBUG_START_MAP_GEN")]
    public void DEBUG_START_MAP_GEN() => CreateNewFloor();
#endif
    #endregion
}