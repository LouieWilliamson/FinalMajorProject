using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject[] Platforms;

    [Range(1, 100)]
    public int PercentSpawnChance;

    private int rand;
    private GameObject platformInstance;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(1, 101); // 1 - 100

        if (rand <= PercentSpawnChance)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Spawn()
    {
        int randomPlatform = Random.Range(0, Platforms.Length);
        
        platformInstance = (GameObject)Instantiate(Platforms[randomPlatform], transform.position, Quaternion.identity);
        platformInstance.transform.parent = transform; //parent the spawned tile to this
        Physics2D.IgnoreLayerCollision(10, 10);
    }
}
