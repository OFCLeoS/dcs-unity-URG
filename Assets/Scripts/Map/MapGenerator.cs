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

    [Tooltip("How far from 0,0 can rooms spawn at")]
    [SerializeField] Vector2Int spawnJitter;

    [Tooltip("How fast can rooms move when collisions are being resolved")]
    [SerializeField] Vector2Int displacementSpeed;

    Map currentFloorMap;

    #region Initialization
    void Awake()
    {
        spawnJitter.x = Mathf.Abs(spawnJitter.x);
        spawnJitter.y = Mathf.Abs(spawnJitter.y);
        displacementSpeed.x = Mathf.Abs(displacementSpeed.x);
        displacementSpeed.y = Mathf.Abs(displacementSpeed.y);
        if (cellSize <= 0)
        {
            StopMapGenDueToError("The Map Generator has an invalid cell size, it will not work!");
        }
    }
    #endregion

    void CreateNewFloor()
    {
        if (!SpawnRooms())
        {
            StopMapGenDueToError("The Map Generator encountered an error when creating the rooms array!");
        }
        // TODO: Process could be changed for different map results? Make them modifable on inspector?

        // We create the map with the newly created Room list
        currentFloorMap = new Map(rooms);
        // We set random layers to all the rooms
        SetLayers();
        // We add spawn jitter to all the rooms
        AddSpawnJitter();
        // We set random displacement vectors to all the rooms
        SetDisplacements();
        // We Resolve all Room Collisions
        ResolveRoomCollisions();
    }


    #region Map Creation Steps
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
    /// Sets a random Layer to all room coordinates
    /// </summary>
    void SetLayers()
    {
        // The maximum layer number will be set to the number of rooms in the map
        int maxLayerNum = rooms.Count;
        foreach (Room room in rooms)
        {
            int roomLayerNumber = Random.Range(0, maxLayerNum + 1);
            room.Layer += roomLayerNumber;
        }
    }

    /// <summary>
    /// Adds a random vector to all room coordinates
    /// </summary>
    void AddSpawnJitter()
    {
        foreach (Room room in rooms)
        {
            Vector2Int roomSpawnJitter = new Vector2Int(Random.Range(-spawnJitter.x, spawnJitter.x + 1), Random.Range(-spawnJitter.y, spawnJitter.y + 1));
            room.Coordinates += roomSpawnJitter;
        }
    }

    /// <summary>
    /// Sets a random Displacement vector to all room coordinates
    /// </summary>
    void SetDisplacements()
    {
        foreach (Room room in rooms)
        {
            Vector2Int roomDisplacementSpeed = new Vector2Int(Random.Range(-displacementSpeed.x, displacementSpeed.x + 1), Random.Range(-displacementSpeed.y, displacementSpeed.y + 1));
            room.Displacement += roomDisplacementSpeed;
        }
    }

    /// <summary>
    /// Pushes rooms away from each other using their Displacement Vectors until no collisions are detected
    /// </summary>
    void ResolveRoomCollisions()
    {
        bool allRoomsPlaced = false;
        int placedRoomsCount = 0;
        while (!allRoomsPlaced)
        {
            // TODO: Could remove placed rooms, but may need them in the list later...
            foreach (Room room in rooms)
            {
                if (room.IsPlaced) continue;
                if (currentFloorMap.RoomIsColliding(room)) room.DisplaceRoom();
                else
                {
                    placedRoomsCount++;
                    room.IsPlaced = true;
                }
                // TODO: Could early stop here with below if statement? Or not worth it?
            }
            if (placedRoomsCount >= rooms.Count) allRoomsPlaced = true;
        }
    }

    #endregion

    // TODO: SEED SYSTEM!!!!!!!!!

    #region DEBUGGING
#if UNITY_EDITOR
    [ContextMenu("DEBUG_START_MAP_GEN")]
    public void DEBUG_START_MAP_GEN() => CreateNewFloor();
#endif
    #endregion

    #region Error Handling
    /// <summary>
    /// Stops the Generation of the Map due to an error.
    /// </summary>
    void StopMapGenDueToError(string errorText)
    {
#if UNITY_EDITOR
        Debug.LogError(errorText);
#endif
        gameObject.SetActive(false);
    }
    #endregion
}