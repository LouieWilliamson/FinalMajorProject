using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOtherRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask roomLayer;
    public LevelGeneration levelGenerator;
    public Transform FillerParent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);
        
        //after the level has been generated if there isn't a room already spawned then spawn a random one
        if (roomDetection == null && levelGenerator.stopBuilding)
        {
            int randomRoom = Random.Range(0, levelGenerator.roomTypes.Length);
            GameObject roomInstance = (GameObject)Instantiate(levelGenerator.roomTypes[randomRoom], transform.position, Quaternion.identity);
            roomInstance.transform.parent = FillerParent;
            Destroy(gameObject);
        }
    }
}
