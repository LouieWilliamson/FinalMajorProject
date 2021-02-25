using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public enum TileType { filler, floor, floorLeft, floorRight, ceiling };

    // Start is called before the first frame update
    public TileType tType;
    public GameObject[] floor;
    public GameObject FloorEdgeL;
    public GameObject FloorEdgeR;

    public GameObject[] ceiling;
    public GameObject[] filler;

    void Start()
    {
        SpawnLevelTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnLevelTile()
    {
        GameObject instance;

        switch (tType)
        {
            case TileType.filler:
                int rand = Random.Range(0, filler.Length);
                instance = (GameObject)Instantiate(filler[rand], transform.position, Quaternion.identity);
                break;
            case TileType.floor:
                int floorRand = Random.Range(0, floor.Length);
                instance = (GameObject)Instantiate(floor[floorRand], transform.position, Quaternion.identity);
                break;
            case TileType.ceiling:
                int ceilingRand = Random.Range(0, ceiling.Length);
                instance = (GameObject)Instantiate(ceiling[ceilingRand], transform.position, Quaternion.identity);
                break;
            case TileType.floorLeft:
                instance = (GameObject)Instantiate(FloorEdgeL, transform.position, Quaternion.identity);
                break;
            case TileType.floorRight:
                instance = (GameObject)Instantiate(FloorEdgeR, transform.position, Quaternion.identity);
                break;
            default:
                int defaultRand = Random.Range(0, filler.Length);
                instance = (GameObject)Instantiate(filler[defaultRand], transform.position, Quaternion.identity);
                print("DEFAULT TILE CHOSEN");
                break;
        }
        
        instance.transform.parent = transform; //parent the spawned tile to this
    }
}
