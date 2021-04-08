using System.Collections;
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
    private GameObject player;
    private HUDManager HUD;
    private Parallax parallax;
    private GameObject vCam;
    private GameObject mainCam;
    private Vector3 centred = new Vector3(0, 0, 1);

    private bool startRoomSaved;

    private PlayerMovement pMove;
    private TeleportEffect teleFX;
    private bool teleportFXactive;
    private void Start()
    {
        levelGen = GameObject.FindObjectOfType<LevelGeneration>();
        parallax = GameObject.Find("Parallax").GetComponent<Parallax>();
        vCam = GameObject.Find("VirtualCamera");
        mainCam = GameObject.Find("Main Camera");

        startRoomSaved = false;

        teleportFXactive = false;
        faceActive = false;
        playerTeleported = false;
    }
    void Update()
    {
        if (levelGen.GetLevelFinished() && !startRoomSaved)
        {
            startRoomSaved = true;
            startRoom = GameObject.FindObjectOfType<StartingRoom>();
            HUD = startRoom.HUD;
            roomSpawn = startRoom.transform;
            player = startRoom.player;
            pMove = player.GetComponent<PlayerMovement>();
            teleFX = player.GetComponent<TeleportEffect>();
        }

        if(faceActive && !playerTeleported && startRoomSaved)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(pMove.hasGun)
                {
                    StartTeleportFX();
                }
                else
                {
                    print("YOU NEED YOUR GUN");
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
        player.transform.position = roomSpawn.position;
        //move cams
        vCam.transform.position = player.transform.position;
        mainCam.transform.position = player.transform.position;
        //move parallax
        parallax.fg.transform.position = player.transform.position;

        startRoom.EnableHUD();
        parallax.SetTeleported();

        teleFX.Reappear();

        playerTeleported = true;
    }
}
