using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public enum TileType { filler, floor, floorLeft, floorRight, ceiling, decor };

    // Start is called before the first frame update
    public TileType tType;
    public GameObject[] floor;
    public GameObject FloorEdgeL;
    public GameObject FloorEdgeR;

    public GameObject[] ceiling;
    public GameObject[] filler;
    public GameObject[] decor;

    private GameObject tileInstance;
    void Start()
    {
        SpawnLevelTile();
    }
    void SpawnLevelTile()
    {
        switch (tType)
        {
            case TileType.filler:
                int rand = Random.Range(0, filler.Length);
                tileInstance = (GameObject)Instantiate(filler[rand], transform.position, Quaternion.identity);
                break;
            case TileType.floor:
                int floorRand = Random.Range(0, floor.Length);
                tileInstance = (GameObject)Instantiate(floor[floorRand], transform.position, Quaternion.identity);
                break;
            case TileType.ceiling:
                int ceilingRand = Random.Range(0, ceiling.Length);
                tileInstance = (GameObject)Instantiate(ceiling[ceilingRand], transform.position, Quaternion.identity);
                break;
            case TileType.floorLeft:
                tileInstance = (GameObject)Instantiate(FloorEdgeL, transform.position, Quaternion.identity);
                break;
            case TileType.floorRight:
                tileInstance = (GameObject)Instantiate(FloorEdgeR, transform.position, Quaternion.identity);
                break;
            case TileType.decor:
                SpawnDecor();
                break;
            default:
                int defaultRand = Random.Range(0, filler.Length);
                tileInstance = (GameObject)Instantiate(filler[defaultRand], transform.position, Quaternion.identity);
                print("NO TILE TYPE CHOSEN");
                break;
        }       
        tileInstance.transform.parent = transform; //parent the spawned tile to this
    }
    private void CheckDecorSpawn()
    {
        //check against amount of decorations
        //random scale between 3 and 5
        //make sure it goes to the floor

    }
    private void SpawnDecor()
    {
        int decorRand = Random.Range(0, decor.Length);
        float randomScale = Random.Range(3f, 5f);
        int randomFlipped = Random.Range(0,2);
        bool flipped = randomFlipped == 0;

        tileInstance = (GameObject)Instantiate(decor[decorRand], transform.position, Quaternion.identity);

        if (flipped) tileInstance.GetComponent<SpriteRenderer>().flipX = true;
        tileInstance.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
}
