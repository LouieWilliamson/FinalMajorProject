using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject[] Platforms;

    [Range(0, 100)]
    public int PercentSpawnChance;

    private int rand;
    private GameObject platformInstance;
    private SpawnDarkOrb darkOrb;
    private int randomPlatform;
    private float orbYoffset;
    private RoomType Room;

    // Start is called before the first frame update
    void Start()
    {
        Room = GetComponentInParent<RoomType>();

        orbYoffset = 1;

        darkOrb = GetComponent<SpawnDarkOrb>();

        rand = Random.Range(1, 101); // 1 - 100

        if (rand <= PercentSpawnChance)
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        if (Room.RoomForPlatform())
        {
            SpawnPform();
            SpawnOrb();
        }
    }
    private void SpawnPform()
    {
        randomPlatform = Random.Range(0, Platforms.Length);

        platformInstance = (GameObject)Instantiate(Platforms[randomPlatform], transform.position, Quaternion.identity);
        platformInstance.transform.parent = transform; //parent the spawned tile to this
        Room.AddPlatform();
    }
    private void SpawnOrb()
    {
        CheckOrbSpawnOffset();
        Vector2 darkOrbPos = new Vector2(transform.position.x, transform.position.y + orbYoffset);
        darkOrb.SpawnOrb(darkOrbPos);
    }
    private void CheckOrbSpawnOffset()
    {
        if(randomPlatform == 0)
        {
            orbYoffset *= 2;
        }
        else if (randomPlatform == 1)
        {
             orbYoffset *= 0.5f;
        }
    }
}
