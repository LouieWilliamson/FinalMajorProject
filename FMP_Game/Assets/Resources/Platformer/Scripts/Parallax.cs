using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject fg;
    public GameObject bg;
    public Rigidbody2D fg_rb;
    public float speed;
    public Camera cam;
    private bool teleported;
    private bool shouldMoveFG;

    private float moveTimer = 0;
    private float moveTime = 1f;
    private void Start()
    {
        shouldMoveFG = false;
        teleported = false;
    }
    private void Update()
    {
        //if player has teleported and fg shouldnt move yet, wait a couple secs then allow it to move
        if (teleported && !shouldMoveFG)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= moveTime)
            {
                shouldMoveFG = true;
            }
        }

        if (shouldMoveFG) 
        { 
            fg_rb.velocity = -cam.velocity * speed; 
        }
    }
    public void SetTeleported() { teleported = true; }
}
