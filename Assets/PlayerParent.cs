using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

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
            // Parent the camera to the player GameObject
            transform.SetParent(playerTransform);
            // Reset local position and rotation to zero
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}

