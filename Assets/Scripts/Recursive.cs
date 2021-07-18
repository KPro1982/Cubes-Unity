using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Recursive : Maze
{
    public override void Generate()
    {
        DateTime start = DateTime.Now;
        Debug.Log($"Starting generation at {start}");
        Generate(5, 5);
        DateTime stop = DateTime.Now;
        Debug.Log($"Finished generation at {stop}, elapsed time {stop-start}");
    }

    void Generate(int x, int z)
    {
        if (CountSquareNeighbours(x, z) <= 1) Recurse(x, z);
        if (CountSquareNeighbours(x, z) == 2)
        {
            int r = Random.Range(1, 100);
            if(Extraspace > r)
            {
                map[x, z] = 0;
            }
        }

        return;
    }

    private void Recurse(int x, int z)
    {
        map[x, z] = 0;

        directions.Shuffle();

        Generate(x + directions[0].x, z + directions[0].z);
        Generate(x + directions[1].x, z + directions[1].z);
        Generate(x + directions[2].x, z + directions[2].z);
        Generate(x + directions[3].x, z + directions[3].z);
    }
}
