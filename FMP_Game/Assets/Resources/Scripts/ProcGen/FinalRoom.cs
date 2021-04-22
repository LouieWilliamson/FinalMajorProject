using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject wallface;
    private RoomType rType;
    void Start()
    {
        wallface = (GameObject)Resources.Load("Prefabs/Guardian");
        rType = GetComponent<RoomType>();

        // 0 ---> (spawnerNum - 1)
        int randomSpawn = Random.Range(0, rType.endSpawners.Length);

        Instantiate(wallface, rType.endSpawners[randomSpawn]);
    }
}
