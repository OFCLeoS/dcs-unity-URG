using System.Collections.Generic;

public class Sector
{
    public float width;
    public float length;

    public float x;
    public float y;

    public List<DoorPoint> doorPoints;

    public Sector(float width, float length, float x, float y)
    {
        this.width = width;
        this.length = length;

        this.x = x;
        this.y = y;
    }
}