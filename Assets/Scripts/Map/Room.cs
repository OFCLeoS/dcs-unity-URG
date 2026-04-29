using UnityEngine;

/// <summary>
/// A representation of a facility room
/// </summary>
public class Room
{
    public int Width { get; }
    public int Height { get; }
    public Vector2Int Coordinates { get; }
    /// <summary>
    /// How much the room is moving per iteration of the map generator.
    /// </summary>
    public Vector2Int Displacement { set; get; }

    #region Initialization
    public Room(int width, int height)
    {
        Width = width;
        Height = height;
        Coordinates = Vector2Int.zero;
        Displacement = Vector2Int.zero;
    }

    public Room(int width, int height, Vector2Int startingPos)
    {
        Width = width;
        Height = height;
        Coordinates = startingPos;
        Displacement = Vector2Int.zero;
    }

    public Room(int width, int height, Vector2Int startingPos, Vector2Int startingDisplacement)
    {
        Width = width;
        Height = height;
        Coordinates = startingPos;
        Displacement = startingDisplacement;
    }
    #endregion

    
}