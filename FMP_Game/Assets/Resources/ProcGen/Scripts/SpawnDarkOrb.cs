using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDarkOrb : MonoBehaviour
{
    [Range(0, 100)]
    public int ChanceToSpawn;

    public GameObject darkOrbPrefab;
    GameObject dOrbInstance;
    public void SpawnOrb(Vector2 position)
    {
        int chance = Random.Range(1, 101);

        if (chance <= ChanceToSpawn)
        {
            dOrbInstance = (GameObject)Instantiate(darkOrbPrefab, position, Quaternion.identity);
            dOrbInstance.transform.parent = gameObject.GetComponentInChildren<PlatformEffector2D>().gameObject.transform;; //parent the spawned orb to the platform
        }
    }
}
