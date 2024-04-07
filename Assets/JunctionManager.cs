using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


