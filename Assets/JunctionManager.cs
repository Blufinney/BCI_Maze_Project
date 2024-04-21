using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// array of all the junctions
// Junction is an array of waypoint arrays
// Waypoint arrays
// 1 2 3 4 waypoints in the 

public class JunctionManager : MonoBehaviour
{
    public CustomJunction[] junctions;

    // Optionally, you can include any other properties or methods related to managing junctions

    private void Awake()
    {
        InitializeTracks();
    }

    private void InitializeTracks()
    {
        foreach (CustomJunction junction in junctions)
        {
            junction.tracks = junction.GetComponentsInChildren<WaypointArray>();
        }
    }
}


