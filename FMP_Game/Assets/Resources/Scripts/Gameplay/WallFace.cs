﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFace : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator FaceAnim;
    bool faceActive;
    internal bool playerTeleported;

    private LevelGeneration levelGen;
    private StartingRoom startRoom;
    private Transform roomSpawn;
    public Transform startRoomSpawn;
    private GameObject player;
    private PlayerTooltip pTooltip;
    private HUDManager HUD;
    private Parallax parallax;
    private GameObject vCam;
    private GameObject mainCam;
    private Vector3 centred = new Vector3(0, 0, 1);

    private bool startRoomSaved;
    private bool playerSaved;

    private PlayerMovement pMove;
    private TeleportEffect teleFX;
    private bool teleportFXactive;
    private DialogueManager dManager;

    private AudioManager aManager;

    public LayerMask platformLayer;
    public float platformDetectionRadius;
    public bool endOfLevel;
    internal GameObject DemoEnd;
    private void Start()
    {
        levelGen = GameObject.FindObjectOfType<LevelGeneration>();
        parallax = GameObject.Find("Parallax").GetComponent<Parallax>();
        vCam = GameObject.Find("VirtualCamera");
        mainCam = GameObject.Find("Main Camera");

        aManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
        dManager = aManager.gameObject.GetComponent<DialogueManager>();

        startRoomSaved = false;
        playerSaved = false;

        teleportFXactive = false;
        faceActive = false;
        playerTeleported = false;

        Collider2D platformDetection = Physics2D.OverlapCircle(transform.position, platformDetectionRadius, platformLayer);
        if (platformDetection != null)
        {
            Destroy(platformDetection.gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, platformDetectionRadius);
    }
    void Update()
    {
        if (levelGen.GetLevelFinished() && !startRoomSaved)
        {
            startRoom = GameObject.FindObjectOfType<StartingRoom>();
            HUD = startRoom.HUD;
            roomSpawn = startRoom.transform;
            startRoomSaved = true;
        }

        if (startRoomSaved && !playerSaved)
        {
            if (startRoom.spawnedPlayer)
            {
                player = startRoom.player;
                pMove = player.GetComponent<PlayerMovement>();
                teleFX = player.GetComponent<TeleportEffect>();
                pTooltip = player.GetComponentInChildren<PlayerTooltip>();
                playerSaved = true;
            }
        }

        if (faceActive && !playerTeleported && startRoomSaved)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(pMove.hasGun)
                {
                    StartTeleportFX();
                }
                else
                {
                    pTooltip.SetTipText(PlayerTooltip.TipType.Gun);
                }
            }
        }

        if (teleportFXactive)
        {
            if (teleFX.disappearFXFinished)
            {
                TeleportToStart();
                teleportFXactive = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FaceAnim.SetTrigger("In");
            faceActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
                FaceAnim.SetTrigger("Out");
                faceActive = false;
        }
    }
    //Dissapear, teleport player, reappear
    private void StartTeleportFX()
    {
        teleFX.Dissapear();
        teleportFXactive = true;
    }
    private void TeleportToStart()
    {
        //move player
        if (endOfLevel)
        {
            player.transform.position = startRoomSpawn.position;
        }
        else
        {
            player.transform.position = roomSpawn.position;
        }

        //move cams
        vCam.transform.position = player.transform.position;
        mainCam.transform.position = player.transform.position;

        //move parallax
        parallax.fg.transform.position = player.transform.position;

        startRoom.EnableHUD();
        parallax.SetTeleported();

        teleFX.Reappear();
        playerTeleported = true;

        aManager.SetMusicTrack(AudioManager.Music.Level1);
        dManager.EndDialogue();
        //end message for demo
        if (endOfLevel)
        {
            aManager.SetMusicTrack(AudioManager.Music.MainMenu);
            DemoEnd.SetActive(true);
        }
    }
}
