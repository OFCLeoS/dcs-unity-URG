using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A representation of a facility floor
/// </summary>
public class Map
{
    /// <summary>
    /// If a Coordinate does not have a list room reference, it means it is empty
    /// </summary>
    public Dictionary<Vector2Int, List<Room>> FloorMap { get; }

    public Map(List<Room> rooms)
    {
        FloorMap = new Dictionary<Vector2Int, List<Room>>();
        List<Room> inspectedCell;
        foreach (Room room in rooms)
        {
            Vector2Int inspectedRoomCoords = room.Coordinates;
            if (!FloorMap.TryGetValue(inspectedRoomCoords, out inspectedCell))
            {
                inspectedCell = new List<Room>();
                FloorMap.Add(inspectedRoomCoords, inspectedCell);
            }
            inspectedCell.Add(room);
        }
    }

    /// <summary>
    /// Checks if a room collides with any other room in the layer below it
    /// </summary>
    /// <returns></returns>
    public bool RoomIsColliding(Room room)
    {
        // TODO: AABB or something else? Should a grid even be used?
        // AABB:
        //   Mathf.Abs(aCenter.x - bCenter.x) <= (aHalfSize.x + bHalfSize.x) &&
        //            Mathf.Abs(aCenter.y - bCenter.y) <= (aHalfSize.y + bHalfSize.y);

        return false;
    }
}