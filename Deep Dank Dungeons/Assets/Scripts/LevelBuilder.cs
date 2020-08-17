using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public Room startRoomPrefab, endRoomPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3, 10);

    List<Doorway> availableDoorways = new List<Doorway>();

    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();

    LayerMask roomLayerMask;
    
    void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        // Place start room
        Debug.Log("Place start room");
        yield return interval;

        // Random iterations
        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for(int i = 0; i < iterations; i++)
        {
            // Place random room from list
            Debug.Log("Place random room from list");
            yield return interval;
        }

        // Place end room
        Debug.Log ("Place end room");
        yield return interval;

        // Level generation finished
        Debug.Log("Level generation finished");

        yield return new WaitForSeconds(3);
        Debug.Log("Reset level generator");
        StopCoroutine("GenerateLevel");
        StartCoroutine("GenerateLevel");
    }
}
