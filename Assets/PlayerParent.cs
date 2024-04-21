using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    [Range(30f, 120f)] // Define the range of acceptable FOV values
    public float fieldOfView = 30f; // Field of View variable

    void Start()
    {
        // Check if playerTransform is assigned
        if (playerTransform == null)
        {
            Debug.LogError("Player transform is not assigned!");
            enabled = false; // Disable the script if playerTransform is not assigned
        }
        else
        {
            // Set the camera's field of view
            GetComponent<Camera>().fieldOfView = fieldOfView;

            // Parent the camera to the player GameObject
            transform.SetParent(playerTransform);
            // Reset local position and rotation to zero
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}

