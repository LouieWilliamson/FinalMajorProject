using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGeneration : MonoBehaviour
{
    private enum direction { left, right, up };

    public Transform[] startPositions;
    public GameObject[] roomTypes;
    // 0 = LR | 1 = LRB | 2 = LRT | 3 = LRTB

    private GameObject currentRoom;

    private direction spawnerDirection;
    private direction previousDirection;

    private int randomDirection;
    private Vector2 newPos;

    public float horizontalMoveAmount, verticalMoveAmount;
    public float minX, maxX, maxY;

    private float moveTimer;
    public float spawnTime;
    internal bool stopBuilding;
    public LayerMask roomLayer;

    private int UpCounter;

    private bool StartRoomSet;
    private int roomCount;
    public Transform CriticalParent;
    void Start()
    {
        StartRoomSet = false;

        roomCount = 0;
        UpCounter = 0;
        stopBuilding = false;
        moveTimer = 0;

        int randomStart = Random.Range(0, startPositions.Length);
        transform.position = startPositions[randomStart].position;

        GetNewRoomType();
        SpawnRoom();
        GetNewDirection();
    }
    void Update()
    {
        //if the generator is still building
        if (!stopBuilding)
        {
            if (moveTimer >= spawnTime)
            {
                MoveSpawner();
                moveTimer = 0;
            }
            else
            {
                moveTimer += Time.deltaTime;
            }
        }
    }

    //Moves the spawner in a given direction and spawns a room in it
    void MoveSpawner()
    {
        switch (spawnerDirection)
        {
            case direction.left:
                UpCounter = 0;
                newPos = new Vector2(transform.position.x - horizontalMoveAmount, transform.position.y);
                transform.position = newPos;
                break;

            case direction.right:
                UpCounter = 0;
                newPos = new Vector2(transform.position.x + horizontalMoveAmount, transform.position.y);
                transform.position = newPos;
                break;

            case direction.up:
                UpCounter++;
                CheckPreviousRoomBeforeMovingUp();
                newPos = new Vector2(transform.position.x, transform.position.y + verticalMoveAmount);
                transform.position = newPos;
                break;

            default:
                print("Error: Invalid Spawner Direction");
                break;
        }

        //store the current direction into previous direction for the calculation of the next move
        previousDirection = spawnerDirection;

        GetNewRoomType();
        SpawnRoom();
        GetNewDirection();
    }
    
    //Generates a random direction for the room spawner to move in
    void GetNewDirection()
    {
        randomDirection = Random.Range(1, 6); // 1 - 5

        if (randomDirection == 1 || randomDirection == 2)       //40% chance to move left
        {
            spawnerDirection = direction.left;
        }
        else if (randomDirection == 3 || randomDirection == 4)  //40% chance to move right
        {
            spawnerDirection = direction.right;
        }
        else                                                    //20% chance to move up
        {
            spawnerDirection = direction.up;
        }


        CheckPreviousDirection();
        CheckEdge();
    }

    //Checks new direction against previous one to ensure rooms arent drawn over each other
    void CheckPreviousDirection()
    {
        int newDirection = Random.Range(1, 6); // 1 - 5

        switch (previousDirection)
        {
            //if it previously moved left
            case direction.left:
                //if new direction is right, change it to left or up
                if (spawnerDirection == direction.right)
                {
                    if (newDirection <= 3)  // 1 - 3 (60% chance to move left again)
                    {
                        spawnerDirection = direction.left;
                    }
                    else                    // 4 - 5 (40% chance to move up)
                    {
                        spawnerDirection = direction.up;
                    }
                }
                break;

            //if it previously moved right
            case direction.right:
                //if new direction is left, change it to right or up
                if (spawnerDirection == direction.left)
                {
                    if (newDirection <= 3)  // 1 - 3 (60% chance to move right again)
                    {
                        spawnerDirection = direction.right;
                    }
                    else                    // 4 - 5 (40% chance to move up)
                    {
                        spawnerDirection = direction.up;
                    }
                }
                break;
            default:
                break;
        }
    }

    //Ensures the level doesn't build rooms outside the level borders
    void CheckEdge()
    {
        //if going left and at the left edge of the level, move up instead.
        if (spawnerDirection == direction.left)
        {
            if (transform.position.x <= minX)
            {
                spawnerDirection = direction.up;
            }
        }
        //if going right and at the right edge of the level, move up instead.
        if (spawnerDirection == direction.right)
        {
            if (transform.position.x >= maxX)
            {
                spawnerDirection = direction.up;
            }
        }
        //if going up and at the top of the level, stop building
        if (spawnerDirection == direction.up)
        {
            if (transform.position.y >= maxY)
            {
                stopBuilding = true;
            }
        }
    }

    //Generate what type of room is to be spawned.
    void GetNewRoomType()
    {
        // 0 = LR | 1 = LRB | 2 = LRT | 3 = LRTB
        int randomRoom = Random.Range(0, roomTypes.Length);

        switch (spawnerDirection)
        {
            case direction.left:
                currentRoom = roomTypes[randomRoom];
                break;
            case direction.right:
                currentRoom = roomTypes[randomRoom];
                break;
            //if direction is up, new room needs to have a bottom opening
            case direction.up: 
                randomRoom = Random.Range(1, 3); // 1 - 2 

                if (randomRoom == 1)
                {
                    randomRoom = 1; //50% chance of being LRB
                }
                else
                {
                    randomRoom = 3; //50% chance of being LRTB
                }
                currentRoom = roomTypes[randomRoom];
                break;
            default:
                break;
        }
        
        //This ensures the starting room does not have a bottom exit
        if (roomCount < 1)
        {
            RoomType rtype = currentRoom.GetComponent<RoomType>();
            if (rtype.type == 1)
            {
                currentRoom = roomTypes[0];
            }
            else if (rtype.type == 3)
            {
                currentRoom = roomTypes[2];
            }
        }
    }

    //Spawns a room in the current spawner position
    void SpawnRoom()
    {
        GameObject roomInstance = (GameObject)Instantiate(currentRoom, transform.position, Quaternion.identity);
        roomInstance.transform.parent = CriticalParent;

        //sets the first spawned room as the starting room
        if (!StartRoomSet)
        {
            roomInstance.AddComponent<StartingRoom>();
            StartRoomSet = true;
        }
        roomCount++;
    }

    //Checks the type of the room before moving up and changes it if necessary
    void CheckPreviousRoomBeforeMovingUp()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);
        RoomType rType = roomDetection.gameObject.GetComponent<RoomType>();

        if (rType.type != 2 && rType.type != 3) //if room type does not have a top opening, destroy it and spawn one with a top opening
        { 
            //if the spawner has move up at least twice, make sure to replace it with a room with all four exit directions
            if (UpCounter >= 2)
            {
                rType.DestroyRoom();
                GameObject roomInstance = (GameObject)Instantiate(roomTypes[3], transform.position, Quaternion.identity);
                roomInstance.transform.parent = CriticalParent;
            }
            else
            {
                rType.DestroyRoom();

                int randUpRoom = Random.Range(2, 4); // 2 - 3
                if (roomCount <= 1) 
                { 
                    randUpRoom = 2; 
                }
                GameObject roomInstance = (GameObject)Instantiate(roomTypes[randUpRoom], transform.position, Quaternion.identity);
                roomInstance.transform.parent = CriticalParent;

                //if the starting room is deleting this makes sure it still has the StartingRoom script
                if (roomCount <= 1)
                {
                    roomInstance.AddComponent<StartingRoom>();
                }
            }
        }
    }
}
