using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a Random Map
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GeneratedRoom> roomsToGenerate;
    List<Room> rooms = new List<Room>();

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
        // TODO!!!

        // We set random layers to all the rooms
        SetLayers();
        // We add spawn jitter to all the rooms
        AddSpawnJitter();
        // We set random displacement vectors to all the rooms
        SetDisplacements();
        // We Resolve all Room Collisions
        ResolveRoomCollisions();
        // We Instantiate all the Rooms in-game
        InstantiateRooms();
    }

    #region Map Creation Steps
    /// <summary>
    /// Adds a representation of all the rooms to be spawned to the rooms array.
    /// </summary>
    /// <returns>True if successful</returns>
    bool SpawnRooms()
    {
        if (roomsToGenerate.Count <= 0) return false;
        rooms.Clear();
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
            room.Position += roomSpawnJitter;
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
                if (RoomIsColliding(room)) room.DisplaceRoom();
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

    /// <summary>
    /// Correctly instantiates all the rooms in the scene depending on their position and the cell size of the map
    /// </summary>
    void InstantiateRooms()
    {
        foreach (Room room in rooms)
        {
            Instantiate(room.RoomObject, new Vector3(room.Position.x * cellSize, 0, room.Position.y * cellSize), Quaternion.identity);
        }
    }

    /// <summary>
    /// Checks if a room collides with any other room in the layer below or equals to it
    /// </summary>
    /// <returns></returns>
    bool RoomIsColliding(Room roomToCheck)
    {
        foreach (Room room in rooms)
        {
            if (room == roomToCheck) continue;
            // We check for IsPlaced because we do not want to place rooms over already placed rooms
            if (room.Layer <= roomToCheck.Layer || room.IsPlaced)
            {
                // AABB Top Left Pivot
                float aXMin = room.Position.x;
                float aXMax = room.Position.x + room.Width;
                float aZMax = room.Position.y;
                float aZMin = room.Position.y - room.Height;

                float bXMin = roomToCheck.Position.x;
                float bXMax = roomToCheck.Position.x + roomToCheck.Width;
                float bZMax = roomToCheck.Position.y;
                float bZMin = roomToCheck.Position.y - roomToCheck.Height;

                bool collides =
                    aXMin < bXMax &&
                    aXMax > bXMin &&
                    aZMin < bZMax &&
                    aZMax > bZMin;

                if (collides) return true;
            }
        }
        return false;
    }

    #endregion

    // TODO: SEED SYSTEM!!!!!!!!!

    #region DEBUGGING
#if UNITY_EDITOR
    public bool DEBUG_START_COLLISION_HANDLING;
    [ContextMenu("DEBUG_START_MAP_GEN")]
    public void DEBUG_START_MAP_GEN() => CreateNewFloor();
    [ContextMenu("DEBUG_MAP_GEN_DEBUGGING")]
    public void DEBUG_MAP_GEN_DEBUGGING() => DEBUG_MAP_GEN();

    Dictionary<Room, Transform> DEBUG_INSTANTIATED_ROOMS = new Dictionary<Room, Transform>();
    public float DEBUG_STEP_DELAY = 0.25f;
    float DEBUG_TIME_SINCE_LAST_STEP = 0;
    float DEBUG_NEXT_STEP_TIME = 0;
    int DEBUG_PLACED_ROOMS = 0;

    void DEBUG_MAP_GEN()
    {
        if (!SpawnRooms())
        {
            StopMapGenDueToError("The Map Generator encountered an error when creating the rooms array!");
        }
        // TODO: Process could be changed for different map results? Make them modifable on inspector?

        // We create the map with the newly created Room list
        // TODO!!!

        // We set random layers to all the rooms
        SetLayers();
        // We add spawn jitter to all the rooms
        AddSpawnJitter();
        // We set random displacement vectors to all the rooms
        SetDisplacements();
        // We Spawn all rooms so that we have visible changes
        foreach (Room room in rooms)
        {
            DEBUG_INSTANTIATED_ROOMS.Add(room, Instantiate(room.RoomObject, new Vector3(room.Position.x * cellSize, 0, room.Position.y * cellSize), Quaternion.identity).transform);
        }
        // We Resolve all Room Collisions
        DEBUG_START_COLLISION_HANDLING = true;
        DEBUG_TIME_SINCE_LAST_STEP = 0;
        DEBUG_NEXT_STEP_TIME = DEBUG_STEP_DELAY;
    }

    void DEBUG_COLLISION_HANDLING_STEP()
    {
        // TODO: Could remove placed rooms, but may need them in the list later...
        foreach (Room room in rooms)
        {
            if (room.IsPlaced) continue;
            if (RoomIsColliding(room))
            {
                room.DisplaceRoom();
                DEBUG_INSTANTIATED_ROOMS[room].position = new Vector3(room.Position.x * cellSize, 0, room.Position.y * cellSize);
            }
            else
            {
                DEBUG_PLACED_ROOMS++;
                room.IsPlaced = true;
            }
            // TODO: Could early stop here with below if statement? Or not worth it?
        }
    }

    void Update()
    {
        if (DEBUG_START_COLLISION_HANDLING)
        {
            DEBUG_TIME_SINCE_LAST_STEP += Time.deltaTime;
            if (DEBUG_TIME_SINCE_LAST_STEP >= DEBUG_NEXT_STEP_TIME)
            {
                DEBUG_COLLISION_HANDLING_STEP();
                DEBUG_TIME_SINCE_LAST_STEP = 0;
                DEBUG_NEXT_STEP_TIME = DEBUG_STEP_DELAY;
            }
            if (DEBUG_PLACED_ROOMS >= rooms.Count)
            {
                DEBUG_START_COLLISION_HANDLING = false;
            }
        }
    }
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