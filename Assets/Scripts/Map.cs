using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int width = 30;
    public int depth = 30;
    public int scale = 6;
    private byte[,] mapArray;
    protected List<GameObject> walls;
    
    public void Start()
    {
        walls = new List<GameObject>();
        Create();
        GE.ActiveMap = this;
        
    }
    public byte[,] MapArray
    {
        get => mapArray;
        set => mapArray = value;
    }


    public Map()
    {
        width = 30;
        depth = 30;
        mapArray = new byte[width, depth];
    }
    public Map(int width, int depth)
    {
        this.width = width;
        this.depth = depth;
        mapArray = new byte[width, depth];
    }

    public virtual void Create()
    {
        
    }
    
}


