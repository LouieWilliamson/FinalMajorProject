using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public GameObject PickupPrefab;
    private GameObject PickupInstance;

    [Range(1, 100)]
    public int PercentSpawnChance;

    private int rand;
    private RoomType Room;

    // Start is called before the first frame update
    void Start()
    {
        Room = GetComponentInParent<RoomType>();

        rand = Random.Range(1, 101); // 1 - 100

        if (rand <= PercentSpawnChance)
        {
            if (Room.RoomForPickup())
            {
                Spawn();
            }
        }
    }
    private void Spawn()
    {
        int randomType = Random.Range(0, 4); //-- 0, 1, 2, 3 --
        PickupType type = (PickupType)randomType;

        PickupInstance = (GameObject)Instantiate(PickupPrefab, transform.position, Quaternion.identity);
        PickupInstance.GetComponent<Pickup>().pType = type;
        PickupInstance.transform.parent = transform;
        Room.AddPickup();
    }
}
