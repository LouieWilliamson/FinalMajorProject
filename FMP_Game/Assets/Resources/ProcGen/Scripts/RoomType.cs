using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public enum RType { LR, LRB, LRT, LRTB };

    public RType type;
    // 0 -- LR
    // 1 -- LRB
    // 2 -- LRT
    // 3 -- LRTB

    private int NumOfPlatforms;
    private int NumOfEnemies;
    private int NumOfPickups;
    private int NumOfHazards;
    private void Start()
    {
        NumOfEnemies = 0;
        NumOfHazards = 0;
        NumOfPickups = 0;
        NumOfPlatforms = 0;


    }
    public void DestroyRoom()
    {
        Destroy(gameObject);
    }
    public void AddEnemy()
    {
        NumOfEnemies++;
    }
    public void AddPlatform()
    {
        NumOfPlatforms++;
    }
    public void AddPickup()
    {
        NumOfPickups++;
    }
    public void AddHazard()
    {
        NumOfHazards++;
    }
}
