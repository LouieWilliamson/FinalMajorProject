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
    // Start is called before the first frame update
    void Start()
    {
        darkOrb = GetComponent<SpawnDarkOrb>();

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
        float orbYoffset = 1;

        platformInstance = (GameObject)Instantiate(Platforms[randomPlatform], transform.position, Quaternion.identity);
        platformInstance.transform.parent = transform; //parent the spawned tile to this
        Physics2D.IgnoreLayerCollision(10, 10);
        if(randomPlatform == 1)
        {
            orbYoffset *= 0.5f;
        }

        Vector2 darkOrbPos = new Vector2(transform.position.x, transform.position.y + orbYoffset);
        darkOrb.SpawnOrb(darkOrbPos);
    }
}
