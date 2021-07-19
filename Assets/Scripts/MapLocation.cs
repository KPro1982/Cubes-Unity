using UnityEngine;

public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public Vector2 ToVector() => new(x, z);

    public static MapLocation operator +(MapLocation a, MapLocation b) => new(a.x + b.x, a.z + b.z);

    public override bool Equals(object obj)
    {
        if (obj == null || !GetType().Equals(obj.GetType()))
        {
            return false;
        }

        return x == ((MapLocation) obj).x && z == ((MapLocation) obj).z;
    }

    public override int GetHashCode() => 0;
}