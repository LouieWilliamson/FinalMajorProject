using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHazard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Hazards;

    [Range(1, 100)]
    public int PercentSpawnChance;

    private int rand;
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
        GameObject hazard;
        hazard = (GameObject)Instantiate(Hazards[0], transform.position, Quaternion.identity);
        hazard.transform.parent = transform;
    }
}
