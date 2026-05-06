using UnityEngine;

/// <summary>
/// A representation of a facility room
/// </summary>
public class Room
{
    public GameObject RoomObject { get; }
    public int Width { get; }
    public int Height { get; }
    public Vector2Int Position { set; get; }
    public int Layer { set; get; }
    /// <summary>
    /// How much the room is moving per iteration of the map generator.
    /// </summary>
    public Vector2Int Displacement { set; get; }

    /// <summary>
    /// Is this Room position final?
    /// </summary>
    public bool IsPlaced { set; get; }

    #region Initialization
    public Room(GameObject roomObject, int width, int height)
    {
        RoomObject = roomObject;
        Width = width;
        Height = height;
        Layer = 0;
        Position = Vector2Int.zero;
        Displacement = Vector2Int.zero;
        IsPlaced = false;
    }

    public Room(GameObject roomObject, int width, int height, int layer)
    {
        RoomObject = roomObject;
        Width = width;
        Height = height;
        Layer = layer;
        Position = Vector2Int.zero;
        Displacement = Vector2Int.zero;
        IsPlaced = false;
    }

    public Room(GameObject roomObject, int width, int height, int layer, Vector2Int startingPos)
    {
        RoomObject = roomObject;
        Width = width;
        Height = height;
        Layer = layer;
        Position = startingPos;
        Displacement = Vector2Int.zero;
        IsPlaced = false;
    }

    public Room(GameObject roomObject, int width, int height, int layer, Vector2Int startingPos, Vector2Int startingDisplacement)
    {
        RoomObject = roomObject;
        Width = width;
        Height = height;
        Layer = layer;
        Position = startingPos;
        Displacement = startingDisplacement;
        IsPlaced = false;
    }
    #endregion

    /// <summary>
    /// Uses the Room's Displacement Vector to change its coordinates
    /// </summary>
    public void DisplaceRoom() => Position += Displacement;
}