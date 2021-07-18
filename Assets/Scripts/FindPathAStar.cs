using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class FindPathAStar : MonoBehaviour
{
    public Maze maze;
    public Material closedMaterial;
    public Material openMaterial;

    List<PathMarker> open = new List<PathMarker>();
    List<PathMarker> closed = new List<PathMarker>();

    public GameObject start;
    public GameObject end;
    public GameObject pathP;

    private PathMarker goalNode;
    private PathMarker startNode;
    private PathMarker lastPos;
    private bool done = false;


    void RemoveAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");
        foreach (GameObject m in markers)
            Destroy(m);
    }

    void BeginSearch()
    {
        done = false;
        RemoveAllMarkers();

        List<MapLocation> locations = new List<MapLocation>();
        for (int z = 1; z < maze.depth - 1; z++)
        {
            for (int x = 1; x < maze.width - 1; x++)
            {
                if (maze.map[x, z] != 1)
                    locations.Add(new MapLocation(x, z));
            }
        }

        locations.Shuffle();

        Vector3 startLocation =
            new Vector3(locations[0].x * maze.scale, 0, locations[0].z * maze.scale);
        GameObject gMarker = Instantiate(start, startLocation, Quaternion.identity);
        startNode = new PathMarker(new MapLocation(locations[0].x, locations[0].z), 0, 0, 0,
            gMarker, null);
        Vector3 goalLocation =
            new Vector3(locations[1].x * maze.scale, 0, locations[1].z * maze.scale);
        goalNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z), 0, 0, 0,
            Instantiate(end, goalLocation, Quaternion.identity), null);

        open.Clear();
        closed.Clear();
        open.Add(startNode);
        lastPos = startNode;
    }

    void Search(PathMarker thisNode)
    {
        if (thisNode.Equals(goalNode))
        {
            done = true;
            return;
        }

        foreach (MapLocation dir  in maze.directions)
        {
            MapLocation neighbor = dir + thisNode.location;
            if (maze.map[neighbor.x, neighbor.z] == 1) continue;
            if (neighbor.x < 1 || neighbor.x >= maze.width || neighbor.z < 1 ||
                neighbor.z >= maze.depth) continue;
            if (IsClosed(neighbor)) continue;

            float G = Vector2.Distance(thisNode.location.ToVector(), neighbor.ToVector()) +
                      thisNode.G;
            float H = Vector2.Distance(neighbor.ToVector(), goalNode.location.ToVector());
            float F = G + H;

            GameObject pathBlock = Instantiate(pathP,
                new Vector3(neighbor.x * maze.scale, 0, neighbor.z * maze.scale),
                Quaternion.identity);
            TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();
            values[0].text = "G: " + G.ToString("0.00");
            values[1].text = "H: " + H.ToString("0.00");
            values[2].text = "F: " + F.ToString("0.00");

            if (!UpdateMarker(neighbor, G, H, F, thisNode))
            {
                open.Add(new PathMarker(neighbor, G, H, F, pathBlock, thisNode));
            }
        }

        open = open.OrderBy(p => p.F).ToList<PathMarker>();
        PathMarker pm = (PathMarker) open.ElementAt(0);
        closed.Add(pm);
        open.RemoveAt(0);
        pm.marker.GetComponent<Renderer>().material = closedMaterial;

        lastPos = pm;
    }

    bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker prt)
    {
        foreach (PathMarker p in open)
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

    bool IsClosed(MapLocation marker)
    {
        foreach (PathMarker p in closed)
        {
            if (p.location.Equals(marker)) return true;
        }

        return false;
    }


    void Start()
    {
    }

    void GetPath()
    {
        RemoveAllMarkers();
        open.Clear();
        InstantiateMarker(startNode, start);
        InstantiateMarker(lastPos, end);
        
        PathMarker begin = lastPos;
        PathMarker nextPos = begin.parent;
        while (!nextPos.Equals(startNode))
        {
            var pathBlock = InstantiateMarker(nextPos, pathP);
            
            open.Add(nextPos);
            nextPos = nextPos.parent;

        } 
    }

    private GameObject InstantiateMarker(PathMarker marker, GameObject prefab)
    {
        GameObject pathBlock = Instantiate(prefab,
            new Vector3(marker.location.x * maze.scale, 0, marker.location.z * maze.scale),
            Quaternion.identity);
        return pathBlock;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) BeginSearch();
        if (Input.GetKeyDown(KeyCode.C) && !done) Search(lastPos);
        if (Input.GetKeyDown(KeyCode.M)) GetPath();
    }
}