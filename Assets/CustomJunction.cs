using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomJunction : MonoBehaviour
{
    public Vector3 coordinates;
    public WaypointArray[] tracks = new WaypointArray[4];
}
