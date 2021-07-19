using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetMap : Map
{
    public override void Create()
    {
        walls.Clear();
        walls.AddRange(collection: GameObject.FindGameObjectsWithTag(tag: "Wall"));
        foreach (var cube in walls)
        {
            int x = (int)Mathf.Floor(f: cube.transform.position.x)/scale;
            int z = (int)Mathf.Floor(f: cube.transform.position.z)/scale;
            MapArray[x, z] = 1;
        }

        PrintArray();
    }

    void PrintArray()
    {
        String[] rows = new String[depth];
        for (int i = 0; i < depth; i++)
        {
            for (int ii = 0; ii < width; ii++)
            {
                rows[i] += MapArray[i, ii];
            
            }
            Debug.Log(message: $"[{i}]: {rows[i]}");
        }
        
    }
}