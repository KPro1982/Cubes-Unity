using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Recursive : RandomMap
{
    public override void Generate()
    {
        var start = DateTime.Now;
        Debug.Log($"Starting generation at {start}");
        Generate(5, 5);
        var stop = DateTime.Now;
        Debug.Log($"Finished generation at {stop}, elapsed time {stop - start}");
    }

    private void Generate(int x, int z)
    {
        var aMap = GE.ActiveMap;
        if (CountSquareNeighbours(x, z) <= 1)
        {
            Recurse(x, z);
        }

        if (CountSquareNeighbours(x, z) == 2)
        {
            var r = Random.Range(1, 100);
            if (Extraspace > r)
            {
                MapArray[x, z] = 0;
            }
        }
    }

    private void Recurse(int x, int z)
    {
        MapArray[x, z] = 0;

        GE.directions.Shuffle();

        Generate(x + GE.directions[0].x, z + GE.directions[0].z);
        Generate(x + GE.directions[1].x, z + GE.directions[1].z);
        Generate(x + GE.directions[2].x, z + GE.directions[2].z);
        Generate(x + GE.directions[3].x, z + GE.directions[3].z);
    }
}