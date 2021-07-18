using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap 
{
   
    void CreateMapFromPreset()
    {
        foreach (GameObject cube in GE.Singleton.Walls)
        {
            int x = (int)cube.transform.position.x ;
            int z = (int)cube.transform.position.z ;
            GE.map[x, z] = 1;
        }
    }
}
