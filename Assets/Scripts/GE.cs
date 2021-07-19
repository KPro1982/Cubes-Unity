using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class GE : MonoBehaviour
{
    public static Map ActiveMap
    {
        get => activeMap;
        set => activeMap = value;
    }

    public VisualElement UIRoot
    {
        get => uiRoot;
        set => uiRoot = value;
    }

    private static GE instance;
    
 
    private static Map activeMap;



    public VisualElement uiRoot;
    
    public static List<MapLocation> directions = new()
    {
        new(1, 0),
        new(0, 1),
        new(-1, 0),
        new(0, -1)
    };
    

    public static GE S
    {
        get
        {
            if (instance == null)
            {
                instance = new GE();
                


            }

            return instance;
        }
    }
}