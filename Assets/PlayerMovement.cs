using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private WaypointManager waypointManager;
    private Transform[] currentTrackWaypoints;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    void Start()
    {
        // Get the WaypointManager component from the WaypointManager GameObject
        waypointManager = GameObject.Find("WaypointManager").GetComponent<WaypointManager>();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveAlongTrack();
        }
        else
        {
            CheckInputForTrackSelection();
        }
    }

    void MoveAlongTrack()
    {
        // Check if there are waypoints to follow
        if (currentTrackWaypoints == null || currentWaypointIndex >= currentTrackWaypoints.Length)
        {
            isMoving = false;
            Debug.Log("End of track reached.");
            return;
        }

        // Move towards the current waypoint
        Vector3 targetPosition = currentTrackWaypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate towards the current waypoint
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex++;

            // Look towards the next waypoint
            if (currentWaypointIndex < currentTrackWaypoints.Length)
            {
                Vector3 nextWaypointDirection = (currentTrackWaypoints[currentWaypointIndex].position - transform.position).normalized;
                Quaternion nextWaypointRotation = Quaternion.LookRotation(nextWaypointDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, nextWaypointRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void CheckInputForTrackSelection()
    {
        // Check for input to select a track
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectTrack(waypointManager.track1Waypoints);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectTrack(waypointManager.track2Waypoints);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectTrack(waypointManager.track3Waypoints);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectTrack(waypointManager.track4Waypoints);
        }
    }

    void SelectTrack(Transform[] trackWaypoints)
    {
        currentTrackWaypoints = trackWaypoints;
        currentWaypointIndex = 0;
        isMoving = true;
        Debug.Log("Started track.");
    }
}



