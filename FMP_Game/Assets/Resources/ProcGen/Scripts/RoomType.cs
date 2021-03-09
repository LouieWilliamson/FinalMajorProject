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

    private int MaxPlatforms;
    private int MaxEnemies;
    private int MaxPickups;
    private int MaxHazards;

    private void Start()
    {
        NumOfEnemies = 0;
        NumOfHazards = 0;
        NumOfPickups = 0;
        NumOfPlatforms = 0;

        //SetSpawnLimits();
        MaxEnemies = 2;
        MaxPlatforms = 3;
        MaxPickups = 1;
    }
    private void SetSpawnLimits()
    {
        switch (type)
        {
            case RType.LR:

                break;
            case RType.LRB:

                break;
            case RType.LRT:

                break;
            case RType.LRTB:

                break;
            default:
                break;
        }
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
    public bool RoomForPlatform()
    {
        return (NumOfPlatforms < MaxPlatforms);
    }
    public bool RoomForEnemy()
    {
        return (NumOfEnemies < MaxEnemies);
    }
    public bool RoomForPickup()
    {
        return (NumOfPickups < MaxPickups);
    }
}
