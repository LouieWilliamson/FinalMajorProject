using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    public int bounceHeight;
    public int bounceHorizontal;
    public int damage;

    private bool playerHit;
    private GameObject player;
    private float bounceTimer;
    public float bounceTime;
    private bool AlreadyHit;
    void Start()
    {
        AlreadyHit = false;
        playerHit = false;
        bounceTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHit)
        {
            bounceTimer += Time.deltaTime;

            if (bounceTimer > bounceTime)
            {
                BouncePlayer();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !AlreadyHit)
        {
            player = collision.gameObject;
            playerHit = true;
            AlreadyHit = true;
            print("col");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AlreadyHit = false;
        }
    }
    private void BouncePlayer()
    {
        //apply bounce
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        float velocityX = rb.velocity.x;
        
        if(velocityX < 0)
        {
            velocityX = -bounceHorizontal;
        }
        else if (velocityX > 0)
        {
            velocityX = bounceHorizontal;
        }
        else if (velocityX == 0)
        {
            int randDirection = Random.Range(1, 3); // 1 or 2

            if(randDirection == 1)
            {
                velocityX = bounceHorizontal;
            }
            else
            {
                velocityX = -bounceHorizontal;
            }
        }

        Vector2 bounce = new Vector2(bounceHorizontal, bounceHeight);
        rb.AddForce(bounce, ForceMode2D.Impulse);

        //apply damage
        Inventory inv = player.GetComponent<Inventory>();
        inv.ChangeHealth(-damage);

        playerHit = false;
        bounceTimer = 0;
    }
}
