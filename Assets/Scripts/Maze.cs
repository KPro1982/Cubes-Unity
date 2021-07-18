using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Maze : MonoBehaviour
{
    public List<MapLocation> directions = new List<MapLocation>() {
                                            new MapLocation(1,0),
                                            new MapLocation(0,1),
                                            new MapLocation(-1,0),
                                            new MapLocation(0,-1) };


    void Start()
    {
        InitialiseMap();
        Generate();
        DrawMap();
    }

    void InitialiseMap()
    {

        for (int z = 0; z < GE.depth; z++)
            for (int x = 0; x < GE.width; x++)
            {
                    GE.map[x, z] = 1;     //1 = wall  0 = corridor
            }
    }

    public virtual void Generate()
    {
        for (int z = 0; z < GE.depth; z++)
        for (int x = 0; x < GE.width; x++)
        {
            if (Random.Range(0, 100) < 50)
                GE.map[x, z] = 0; //1 = wall  0 = corridor
        }
    }

    void DrawMap()
    {
        for (int z = 0; z < GE.depth; z++)
            for (int x = 0; x < GE.width; x++)
            {
                if (GE.map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * GE.scale, 0, z * GE.scale);
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.localScale = new Vector3(GE.scale, GE.scale, GE.scale);
                    wall.transform.position = pos;
                }
            }
    }

    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= GE.width - 1 || z <= 0 || z >= GE.depth - 1) return 5;
        if (GE.map[x - 1, z] == 0) count++;
        if (GE.map[x + 1, z] == 0) count++;
        if (GE.map[x, z + 1] == 0) count++;
        if (GE.map[x, z - 1] == 0) count++;
        return count;
    }

    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= GE.width - 1 || z <= 0 || z >= GE.depth - 1) return 5;
        if (GE.map[x - 1, z - 1] == 0) count++;
        if (GE.map[x + 1, z + 1] == 0) count++;
        if (GE.map[x - 1, z + 1] == 0) count++;
        if (GE.map[x + 1, z - 1] == 0) count++;
        return count;
    }

    public int CountAllNeighbours(int x, int z)
    {
        return CountSquareNeighbours(x,z) + CountDiagonalNeighbours(x,z);
    }
}
