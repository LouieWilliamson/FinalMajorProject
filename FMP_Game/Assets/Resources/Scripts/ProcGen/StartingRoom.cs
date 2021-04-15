using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : MonoBehaviour
{
    private StartState state;

    //spawn the player
    private GameObject playerPrefab;
    internal GameObject player;
    private Cinemachine.CinemachineVirtualCamera cam;
    private LevelGeneration lvlGenerator;
    private bool spawnedPlayer;
    private bool levelLoaded;
    private GameObject[] enemies;
    internal HUDManager HUD;
    private GamestateManager gsManager;
    private Transform startSpawn;
    private Parallax parallax;
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
        levelLoaded = false;
        cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        playerPrefab = (GameObject)Resources.Load("Prefabs/Characters/Player");
        HUD = GameObject.Find("Canvas").GetComponent<HUDManager>();
        gsManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GamestateManager>();
        lvlGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();
        state = lvlGenerator.state;
        startSpawn = lvlGenerator.startRoomSpawn;
        parallax = GameObject.Find("Parallax").GetComponent<Parallax>();

        //gun spawning
        gunPowerup = (GameObject)Resources.Load("Prefabs/FX/GunPowerup");
        gunYoffset = 2.5f;
        gunX = transform.position.x + Random.Range(-6, 7);
        gunY = transform.position.y - gunYoffset;
        gunSpawn = new Vector3(gunX, gunY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        levelLoaded = lvlGenerator.GetLevelFinished();

        //if the level is built and the player hasn't been spawned
        if (levelLoaded && !spawnedPlayer)
        {
            if (state == StartState.testing)
            {
                EnableHUD();
                SpawnPlayer(transform);
                SetCameraFollowPlayer();
                parallax.SetTeleported();
                SpawnGun();
            }
            else if (state == StartState.fullgameflow)
            {
                SpawnPlayer(startSpawn);
                SetCameraFollowPlayer();
                HUD.SetLevelLoaded();
            }
        }
    }

    void SpawnPlayer(Transform spawnTransform)
    {
        player = (GameObject)Instantiate(playerPrefab, spawnTransform.position, Quaternion.identity);
        spawnedPlayer = true;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //set enemies to ignore player
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyMovement eMove = enemies[i].GetComponent<EnemyMovement>();
            eMove.SetIgnorePlayer(player);
            eMove.ActivateGravity();
        }
        gsManager.SetPlayerSpawned();
    }

    void SpawnGun()
    {
        GameObject gun;
        gun = (GameObject)Instantiate(gunPowerup, gunSpawn, Quaternion.identity);
        gun.transform.localScale = new Vector3(3, 3, 3);
    }
    public void SetCameraFollowPlayer()
    {
        cam.Follow = player.transform;
    }
    public void EnableHUD()
    {
        HUD.EnableHUD();
        HUD.SetLevelLoaded();
        HUD.SetRunning(true);
    }
}
