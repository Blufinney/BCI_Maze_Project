using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    public Transform doorTransform; // The door's transform
    public Transform playerTransform; // The player's transform
    public float activationDistance = 5.0f; // Distance within which the door will open

    public float openAngle = 90.0f; // Angle the door opens to
    public float openSpeed = 2.0f; // How fast the door opens
    private Quaternion closedRotation;
    private Quaternion openRotationClockwise;
    private Quaternion openRotationCounterClockwise;
    private Quaternion targetRotation;
    private bool isPlayerClose = false;

    void Start()
    {
        if (doorTransform == null)
        {
            Debug.LogError("Door Transform is not assigned.", this);
            return;
        }

        // Store the initial rotation as the closed position
        closedRotation = doorTransform.rotation;
        // Calculate both open rotations based on the closedRotation
        openRotationClockwise = Quaternion.Euler(doorTransform.eulerAngles.x, doorTransform.eulerAngles.y + openAngle, doorTransform.eulerAngles.z);
        openRotationCounterClockwise = Quaternion.Euler(doorTransform.eulerAngles.x, doorTransform.eulerAngles.y - openAngle, doorTransform.eulerAngles.z);
        // Initially, the target rotation is the closed rotation
        targetRotation = closedRotation;
    }

    void Update()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= activationDistance)
        {
            if (!isPlayerClose)
            {
                Debug.Log("Player is within activation distance.");
                isPlayerClose = true;

                // Determine the direction the player is coming from
                Vector3 directionToPlayer = playerTransform.position - transform.position;
                float dotProduct = Vector3.Dot(transform.right, directionToPlayer);

                if (dotProduct > 0)
                {
                    // Player is approaching from one side
                    targetRotation = openRotationClockwise;
                }
                else
                {
                    // Player is approaching from the opposite side
                    targetRotation = openRotationCounterClockwise;
                }
            }
        }
        else if (isPlayerClose)
        {
            Debug.Log("Player has moved out of the activation distance.");
            isPlayerClose = false;
            targetRotation = closedRotation; // Set the target rotation to close the door
        }

        // Smoothly rotate the door towards the target rotation
        doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }
}




