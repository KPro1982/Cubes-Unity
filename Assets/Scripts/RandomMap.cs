using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMap : Map
{
    public int Extraspace = 50;



    public override void Create()
    {
        InitializeMap();
        Generate();
        InstantiateMap();

    }

    private void InitializeMap()
    {

        for (var z = 0; z < depth; z++)
        for (var x = 0; x < width; x++)
            MapArray[x, z] = 1; //1 = wall  0 = corridor
    }

    public virtual void Generate()
    {
        var aMap = GE.ActiveMap;
        for (var z = 0; z < depth; z++)
        {
            for (var x = 0; x < width; x++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    MapArray[x, z] = 0; //1 = wall  0 = corridor
                }
            }
        }
    }

    private void InstantiateMap()
    {
        for (var z = 0; z < depth; z++)
        for (var x = 0; x < width; x++)
            if (MapArray[x, z] == 1)
            {
                var pos = new Vector3(x * scale, 0, z * scale);
                var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall.transform.localScale = new Vector3(scale, scale, scale);
                wall.transform.position = pos;
                walls.Add(wall);
            }
    }

    public int CountSquareNeighbours(int x, int z)
    {
        var count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
        {
            return 5;
        }

        if (MapArray[x - 1, z] == 0)
        {
            count++;
        }

        if (MapArray[x + 1, z] == 0)
        {
            count++;
        }

        if (MapArray[x, z + 1] == 0)
        {
            count++;
        }

        if (MapArray[x, z - 1] == 0)
        {
            count++;
        }

        return count;
    }

    public int CountDiagonalNeighbours(int x, int z)
    {
        var count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
        {
            return 5;
        }

        if (MapArray[x - 1, z - 1] == 0)
        {
            count++;
        }

        if (MapArray[x + 1, z + 1] == 0)
        {
            count++;
        }

        if (MapArray[x - 1, z + 1] == 0)
        {
            count++;
        }

        if (MapArray[x + 1, z - 1] == 0)
        {
            count++;
        }

        return count;
    }

    public int CountAllNeighbours(int x, int z) =>
        CountSquareNeighbours(x, z) + CountDiagonalNeighbours(x, z);
}