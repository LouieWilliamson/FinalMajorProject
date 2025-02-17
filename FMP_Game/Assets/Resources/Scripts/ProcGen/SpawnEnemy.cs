﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] Enemies;
    private GameObject enemyInstance;

    [Range(1, 100)]
    public int PercentSpawnChance;

    private int rand;
    private RoomType Room;
    private GameObject EnemyParent;

    // Start is called before the first frame update
    void Start()
    {
        EnemyParent = GameObject.Find("Enemies");

        Room = GetComponentInParent<RoomType>();

        rand = Random.Range(1, 101); // 1 - 100

        if (rand <= PercentSpawnChance)
        {
            if (Room.RoomForEnemy())
            {
                Spawn();
            }
        }
    }
    private void Spawn()
    {
        int randomEnemy = Random.Range(0, Enemies.Length); // 0 - 4

        enemyInstance = Instantiate(Enemies[randomEnemy], transform.position, Quaternion.identity);
        //enemyInstance.transform.parent = EnemyParent.transform;
        enemyInstance.transform.parent = transform;
        Room.AddEnemy();
        Destroy(this);
    }
}
