using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public Transform trackPrefab; // Prefab for track segments
    public int junctionLength = 5; // Length of each track segment
    public int numJunctions = 10; // Number of junctions in the maze
    public float trackSpacing = 2f; // Spacing between track segments
    public Transform player; // Reference to the player GameObject
    public KeyCode[] trackSelectionKeys; // Keys for selecting new tracks

    private Transform[][] allTracks;
    private Transform[] currentTrack;
    private int currentTrackIndex = -1; // No track selected initially
    private int currentWaypointIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        // Initialize the array of all tracks
        allTracks = new Transform[numJunctions][];

        // Generate tracks for each junction
        for (int i = 0; i < numJunctions; i++)
        {
            allTracks[i] = GenerateTracks(i);
        }
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
        // Your existing logic for moving along the track
    }

    void CheckInputForTrackSelection()
    {
        // Check for input to select a track
        for (int i = 0; i < trackSelectionKeys.Length; i++)
        {
            if (Input.GetKeyDown(trackSelectionKeys[i]) && i != currentTrackIndex)
            {
                SelectTrack(i);
                break;
            }
        }
    }

    void SelectTrack(int trackIndex)
    {
        currentTrackIndex = trackIndex;
        currentTrack = allTracks[currentWaypointIndex];
        currentWaypointIndex = 0;
        isMoving = true;
        Debug.Log("Started track " + (trackIndex + 1));
    }

    Transform[] GenerateTracks(int junctionIndex)
    {
        // Generate tracks for the junction
        Transform[] tracks = new Transform[4]; // Four options at each junction

        for (int i = 0; i < 4; i++)
        {
            // Generate track segment
            Transform track = Instantiate(trackPrefab, transform.position + (transform.forward * junctionLength * junctionIndex), Quaternion.identity);
            // Rotate the track segment
            track.Rotate(Vector3.up, 90f * i);
            // Set the parent of the track segment
            track.SetParent(transform);
            // Add track segment to the array
            tracks[i] = track;
        }

        return tracks;
    }
}

