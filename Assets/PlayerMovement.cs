using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JunctionManager junctionManager;
    public float rotationSpeed = 5f;
    public float speed = 5f;
    public float junctionDetectionRadius = 3f;

    private Transform[] currentTrack;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private Transform detectedJunction = null;

    private void Start()
    {
        // Start with the first track at the initial junction
        currentTrack = junctionManager.junctions[0].tracks[0].waypoints;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToNextWaypoint();
        }
        else
        {
            int input = GetInput();
            if (input > 0 && input <= 4)
            {
                if (detectedJunction != null)
                {
                    int trackIndex = input - 1;
                    if (trackIndex < detectedJunction.GetComponent<CustomJunction>().tracks.Length)
                    {
                        currentTrack = detectedJunction.GetComponent<CustomJunction>().tracks[trackIndex].waypoints;
                        currentWaypointIndex = 0; // Reset waypoint index
                        isMoving = true;
                    }
                }
            }
        }
        UpdateDetectedJunction();
    }

    private void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < currentTrack.Length)
        {
            Transform targetWaypoint = currentTrack[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            Vector3 direction = (targetWaypoint.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= currentTrack.Length)
                {
                    isMoving = false; // Stop moving when reaching the last waypoint
                }
            }
        }
    }

    private void UpdateDetectedJunction()
    {
        foreach (var junction in junctionManager.junctions)
        {
            if (Vector3.Distance(transform.position, junction.coordinates) <= junctionDetectionRadius)
            {
                detectedJunction = junction.transform;
                Debug.Log("Player is at junction: " + junction.name);
                return;
            }
        }
        detectedJunction = null;
    }

    private int GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) return 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) return 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) return 4;
        return 0;
    }
}


