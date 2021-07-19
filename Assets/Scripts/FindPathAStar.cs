using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindPathAStar : MonoBehaviour
{
    public Map activeMap = GE.ActiveMap;
    public Material closedMaterial;
    public Material openMaterial;

    public GameObject start;
    public GameObject end;
    public GameObject pathP;
    private readonly List<PathMarker> closed = new();
    private bool done;

    private PathMarker goalNode;
    private PathMarker lastPos;

    private List<PathMarker> open = new();
    private PathMarker startNode;


    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            BeginSearch();
        }

        if (Input.GetKeyDown(KeyCode.C) && !done)
        {
            Search(lastPos);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GetPath();
        }
    }


    private void RemoveAllMarkers()
    {
        var markers = GameObject.FindGameObjectsWithTag("marker");
        foreach (var m in markers)
        {
            Destroy(m);
        }
    }

    private void BeginSearch()
    {
        done = false;
        RemoveAllMarkers();

        var locations = new List<MapLocation>();
        for (var z = 1; z < activeMap.depth - 1; z++)
        for (var x = 1; x < activeMap.width - 1; x++)
            if (GE.ActiveMap.MapArray[x, z] != 1)
            {
                locations.Add(new MapLocation(x, z));
            }

        locations.Shuffle();

        var startLocation = new Vector3(locations[0].x * activeMap.scale, 0, locations[0].z * activeMap.scale);
        var gMarker = Instantiate(start, startLocation, Quaternion.identity);
        startNode = new PathMarker(new MapLocation(locations[0].x, locations[0].z), 0, 0, 0,
            gMarker, null);
        var goalLocation = new Vector3(locations[1].x * activeMap.scale, 0, locations[1].z * activeMap.scale);
        goalNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z), 0, 0, 0,
            Instantiate(end, goalLocation, Quaternion.identity), null);

        open.Clear();
        closed.Clear();
        open.Add(startNode);
        lastPos = startNode;
    }

    private void Search(PathMarker thisNode)
    {
        if (thisNode.Equals(goalNode))
        {
            done = true;
            return;
        }

        foreach (var dir in GE.directions)
        {
            var neighbor = dir + thisNode.location;
            if (GE.ActiveMap.MapArray[neighbor.x, neighbor.z] == 1)
            {
                continue;
            }

            if (neighbor.x < 1 || neighbor.x >= activeMap.width || neighbor.z < 1 ||
                neighbor.z >= activeMap.depth)
            {
                continue;
            }

            if (IsClosed(neighbor))
            {
                continue;
            }

            var G = Vector2.Distance(thisNode.location.ToVector(), neighbor.ToVector()) +
                    thisNode.G;
            var H = Vector2.Distance(neighbor.ToVector(), goalNode.location.ToVector());
            var F = G + H;

            var pathBlock = Instantiate(pathP,
                new Vector3(neighbor.x * activeMap.scale, 0, neighbor.z * activeMap.scale), Quaternion.identity);
            var values = pathBlock.GetComponentsInChildren<TextMesh>();
            values[0].text = "G: " + G.ToString("0.00");
            values[1].text = "H: " + H.ToString("0.00");
            values[2].text = "F: " + F.ToString("0.00");

            if (!UpdateMarker(neighbor, G, H, F, thisNode))
            {
                open.Add(new PathMarker(neighbor, G, H, F, pathBlock, thisNode));
            }
        }

        open = open.OrderBy(p => p.F).ToList();
        var pm = open.ElementAt(0);
        closed.Add(pm);
        open.RemoveAt(0);
        pm.marker.GetComponent<Renderer>().material = closedMaterial;

        lastPos = pm;
    }

    private bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker prt)
    {
        foreach (var p in open)
        {
            if (p.location.Equals(pos))
            {
                p.G = g;
                p.H = h;
                p.F = f;
                p.parent = prt;
                return true;
            }
        }

        return false;
    }

    private bool IsClosed(MapLocation marker)
    {
        foreach (var p in closed)
        {
            if (p.location.Equals(marker))
            {
                return true;
            }
        }

        return false;
    }

    private void GetPath()
    {
        RemoveAllMarkers();
        open.Clear();
        InstantiateMarker(startNode, start);
        InstantiateMarker(lastPos, end);

        var begin = lastPos;
        var nextPos = begin.parent;
        while (!nextPos.Equals(startNode))
        {
            var pathBlock = InstantiateMarker(nextPos, pathP);

            open.Add(nextPos);
            nextPos = nextPos.parent;
        }
    }

    private GameObject InstantiateMarker(PathMarker marker, GameObject prefab)
    {
        var pathBlock = Instantiate(prefab,
            new Vector3(marker.location.x * activeMap.scale, 0, marker.location.z * activeMap.scale),
            Quaternion.identity);
        return pathBlock;
    }
}