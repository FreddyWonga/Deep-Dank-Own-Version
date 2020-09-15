using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelBuilder : MonoBehaviour
{
    public Room startRoomPrefab, endRoomPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3, 10);
    public PlayerController playerPrefab;
    public StateManager enemyPrefab;
    public LinkedList<Transform> enemySpawnLocation;

    List<Doorway> availableDoorways = new List<Doorway>();

    EnemySpawn enemySpawn;
    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();
    LayerMask roomLayerMask;
    PlayerController player;
    StateManager enemy;

    public NavMeshSurface[] surfaces;

    private void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        //place start room
        PlaceStartRoom();
        yield return interval;

        //random intteration
        int interations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for(int i = 0; i < interations; i++)
        {
            //place random room from list
            PlaceRoom();
            yield return interval;
        }

        //place end room
        PlaceEndRoom();
        yield return interval;

        //Generates the NAVMESH
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }

        //level generator finished 
        Debug.Log("level generator finished");

        //place player
        player = Instantiate(playerPrefab) as PlayerController;
        player.transform.position = startRoom.playerStart.position;
        player.transform.rotation = startRoom.playerStart.rotation;

    }
    void PlaceStartRoom()
    {
        //instanticate room
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;

        //get doorways from current room and add the randomly to the list of availible dorways
        AddDorwaysToList(startRoom, ref availableDoorways);

        //position room
        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;
    }

    void AddDorwaysToList(Room room, ref List<Doorway> list)
    {
        foreach (Doorway doorway in room.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);
        }
    }

    void PlaceRoom()
    {
        //instatiate room
        Room currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)]) as Room;
        currentRoom.transform.parent = this.transform;

        //create dorway list to loop over
        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        List<Doorway> currentRoomDoorways = new List<Doorway>();
        AddDorwaysToList(currentRoom, ref currentRoomDoorways);

        //get doorways from current room and add them randomly to the list of available doorways
        AddDorwaysToList(currentRoom, ref availableDoorways);

        bool roomPlaced = false;

        //try all available doorways
        foreach(Doorway availableDoorway in allAvailableDoorways)
        {
            //try all available doorways in current room
            foreach(Doorway currentDoorway in currentRoomDoorways)
            {
                //position room
                PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);

                //check for overlap
                if (CheckRoomOverlap(currentRoom))
                {
                    continue;
                }

                roomPlaced = true;

                //add room to list
                placedRooms.Add(currentRoom);

                //remove occupied doorways
                currentDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(currentDoorway);

                availableDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(availableDoorway);

                //Spawns in the enemy in the room
                currentRoom.GetComponent<EnemySpawn>().EnemySpawner();

                //exit the loop
                break;
            }
            //exit loop if room has been placed
            if (roomPlaced)
            {
                break;
            }
        }
        //room couldnt be palced restart and try again
        if (!roomPlaced)
        {
            Destroy(currentRoom.gameObject);
            ResetLevelGenerator();
        }
    }

    void PositionRoomAtDoorway(ref Room room, Doorway roomDoorway, Doorway targetDoorway)
    {
        //reset room position and rotation
        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;

        //rotate room to match previous doorway orientation
        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 roomDoorwayEuler = roomDoorway.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180f, 0);

        //position room
        Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;
        room.transform.position = targetDoorway.transform.position - roomPositionOffset;
    }

    bool CheckRoomOverlap(Room room)
    {
        Bounds bounds = room.RoomBounds;
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center = room.transform.position, bounds.size / 2, room.transform.rotation, roomLayerMask);
        if(colliders.Length > 0)
        {
            // ignore collisions with current room
            foreach(Collider c in colliders)
            {
                if (c.transform.parent.gameObject.Equals(room.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Overlap detected");
                    return true;
                }
            }
        }
        return false;
    }

    void PlaceEndRoom()
    {
        //instatiate room
        endRoom = Instantiate(endRoomPrefab) as EndRoom;
        endRoom.transform.parent = this.transform;

        //create dorway list to loop over
        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        Doorway doorway = endRoom.doorways[0];

        bool roomPlaced = false;

        //try all available doorways
        foreach (Doorway availableDoorway in allAvailableDoorways)
        {
            //position room
            Room room = (Room)endRoom;
            PositionRoomAtDoorway(ref room, doorway, availableDoorway);

            //check for overlap
            if (CheckRoomOverlap(endRoom))
            {
                continue;
            }

            roomPlaced = true;

            //remove occupied doorways
            doorway.gameObject.SetActive(false);
            availableDoorways.Remove(doorway);

            availableDoorway.gameObject.SetActive(false);
            availableDoorways.Remove(availableDoorway);

            //exit the loop
            break;
        }

        //room couldnt be palced restart and try again
        if (!roomPlaced)
        {
            ResetLevelGenerator();
        }
    }

    public void ResetLevelGenerator()
    {
        Debug.LogError("reset level gererator");

        StopCoroutine("GenerateLevel");

        //Delete all rooms
        if (startRoom)
        {
            Destroy(startRoom.gameObject);
        }

        if (endRoom)
        {
            Destroy(endRoom.gameObject);
        }

        if (player)
        {
            Destroy(player.gameObject);
        }

        foreach (Room room in placedRooms)
        {
            Destroy(room.gameObject);
        }

        //clear lists
        placedRooms.Clear();
        availableDoorways.Clear();

        //Reset Coroutine
        StartCoroutine("GenerateLevel");

    }
}
