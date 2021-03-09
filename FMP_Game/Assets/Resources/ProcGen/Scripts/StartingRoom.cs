using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : MonoBehaviour
{
    //spawn the player
    private GameObject playerPrefab;
    private Cinemachine.CinemachineVirtualCamera cam;
    private LevelGeneration lvlGenerator;
    private bool spawnedPlayer;
    private GameObject[] enemies;
    private HUDManager HUD;
    private GamestateManager gsManager;

    //spawn gun powerup
    private Vector3 gunSpawn;
    private float gunX;
    private float gunY;
    private float gunYoffset;
    private GameObject gunPowerup;

    // Start is called before the first frame update
    void Start()
    {
        //player spawning
        spawnedPlayer = false;
        lvlGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();
        cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        playerPrefab = (GameObject)Resources.Load("Platformer/Prefabs/Player");
        HUD = GameObject.Find("Canvas").GetComponent<HUDManager>();
        gsManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GamestateManager>();

        //gun spawning
        gunPowerup = (GameObject)Resources.Load("Platformer/Prefabs/GunPowerup");
        gunYoffset = 2.5f;
        gunX = transform.position.x + Random.Range(-6, 7);
        gunY = transform.position.y - gunYoffset;
        gunSpawn = new Vector3(gunX, gunY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //if the level is built and the player hasn't been spawned
        if (lvlGenerator.stopBuilding && !spawnedPlayer)
        {
            HUD.SetLevelLoaded();
            SpawnPlayer();
            SpawnGun();
        }
    }

    void SpawnPlayer()
    {
        GameObject player;
        player = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
        cam.Follow = player.transform;
        spawnedPlayer = true;

        //setting the player values for suitable testing in the proc gen world  ---- SET THESE LATER IN THE PREFAB INSPECTOR

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //set enemies to ignore player
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyMovement eMove = enemies[i].GetComponent<EnemyMovement>();
            eMove.SetIgnorePlayer(player);
            eMove.ActivateGravity();
        }
        HUD.SetRunning(true);
        gsManager.SetPlayerSpawned();
    }

    void SpawnGun()
    {
        GameObject gun;
        gun = (GameObject)Instantiate(gunPowerup, gunSpawn, Quaternion.identity);
        gun.transform.localScale = new Vector3(3, 3, 3);
    }
}
