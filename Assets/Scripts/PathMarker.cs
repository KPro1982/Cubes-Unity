using UnityEngine;

public class PathMarker : MonoBehaviour
{
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMarker parent;
    public MapLocation location;

    public PathMarker(MapLocation l, float g, float h, float f, GameObject marker, PathMarker p)
    {
        location = l;
        G = g;
        H = h;
        F = f;
        this.marker = marker;
        parent = p;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !GetType().Equals(obj.GetType()))
        {
            return false;
        }

        return
            location.Equals(((PathMarker) obj)
                .location); // seems like a bug to me. Should be (location.Equals((MapLocation)obj)).
    }

    public override int GetHashCode() => 0;
}