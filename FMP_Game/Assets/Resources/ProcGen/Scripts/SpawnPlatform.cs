using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject[] Platforms;

    [Range(1, 100)]
    public int PercentSpawnChance;

    private int rand;

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

    }
}
