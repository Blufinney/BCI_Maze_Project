using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target; // Reference to the player GameObject

    public Vector3 offset; // Offset between the camera and the player
    

    void LateUpdate()
    {
        // Check if the player GameObject is assigned
        if (target != null)
        {
            // Set the position of the camera to match the position of the player
            transform.position = target.position + offset;
        }
    }
}
