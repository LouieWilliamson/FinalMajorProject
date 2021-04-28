using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateManager : MonoBehaviour
{
    private GameObject deathscreen;
    private Inventory inv;
    private HUDManager hud;
    private GameObject player;

    private bool gameWon;
    private bool gameOver;
    private bool playerSpawned;
    private bool invSaved;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawned = false;
        invSaved = false;

        deathscreen = GameObject.Find("Canvas").transform.Find("Deathscreen").gameObject;

        hud = GameObject.Find("Canvas").GetComponent<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSpawned && !invSaved)
        {
            inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            player = inv.gameObject;
            invSaved = true;
        }
        if (gameOver)
        {
            player.GetComponent<PlayerAnimations>().SetDead();
            player.GetComponent<TeleportEffect>().Dissapear();
            Time.timeScale = 0.5f;
            deathscreen.SetActive(true);
        }
        else if (gameWon)
        {
            //enable arrow to exit?
        }
    }
    public void SetPlayerSpawned() { playerSpawned = true; }
    public void GameOver()
    {
        gameOver = true;
    }
    public void GameWon()
    {
        gameWon = true;
    }
}
