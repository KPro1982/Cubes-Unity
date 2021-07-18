using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class GE
{
    private static GE instance;
    private List<GameObject> walls = new List<GameObject>();
    public static byte[,] map;
    public static int width = 30; //x length
    public static int depth = 30; //z length

    public static int scale = 6;
    public static int Extraspace = 50;
    public List<GameObject> Walls
    {
        get { return walls; }
    }

    public static GE Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GE();
                instance.Walls.AddRange(GameObject.FindGameObjectsWithTag("Wall"));
                map = new byte[width,depth];
            }

            return instance;
        }
    }
    
}
