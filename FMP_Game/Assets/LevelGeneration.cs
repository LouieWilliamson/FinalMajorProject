using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGeneration : MonoBehaviour
{
    private enum direction { left, right, up };

    public Transform[] startPositions;
    public GameObject[] roomTypes;

    private direction spawnerDirection;
    private int randomDirection;
    private Vector2 newPos;
    public float horizontalMoveAmount;
    public float verticalMoveAmount;

    void Start()
    {
        int randomStart = Random.Range(0, startPositions.Length);
        transform.position = startPositions[randomStart].position;
        Instantiate(roomTypes[0], transform.position, Quaternion.identity);

        GetDirection();
    }

    void MoveSpawner()
    {
        switch (spawnerDirection)
        {
            case direction.left:
                newPos = new Vector2(transform.position.x - horizontalMoveAmount, transform.position.y);
                transform.position = newPos;
                break;
            case direction.right:
                newPos = new Vector2(transform.position.x + horizontalMoveAmount, transform.position.y);
                transform.position = newPos;


                break;
            case direction.up:
                newPos = new Vector2(transform.position.x, transform.position.y + verticalMoveAmount);
                transform.position = newPos;


                break;
            default:
                break;
        }
    }
    void GetDirection()
    {
        randomDirection = Random.Range(1, 6); // 1 - 5

        if (randomDirection == 1 || randomDirection == 2)
        {
            spawnerDirection = direction.left;
        }
        else if (randomDirection == 3 || randomDirection == 4)
        {
            spawnerDirection = direction.right;
        }
        else
        {
            spawnerDirection = direction.up;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
