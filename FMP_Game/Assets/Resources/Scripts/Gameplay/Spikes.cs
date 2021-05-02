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
        if (rb == null) rb = player.GetComponentInParent<Rigidbody2D>();

        float velocityX = rb.velocity.x;
        float bounceX = 0;

        if(velocityX < 0)
        {
            bounceX = -bounceHorizontal;
        }
        else if (velocityX > 0)
        {
            bounceX = bounceHorizontal;
        }
        else if (velocityX == 0)
        {
            int randDirection = Random.Range(1, 3); // 1 or 2

            if(randDirection == 1)
            {
                bounceX = bounceHorizontal;
            }
            else
            {
                bounceX = -bounceHorizontal;
            }
        }
        Vector2 bounce = new Vector2(bounceX, bounceHeight);
        rb.AddForce(bounce, ForceMode2D.Impulse);

        //apply damage
        Inventory inv = player.GetComponent<Inventory>();
        if (inv == null) inv = player.GetComponentInParent<Inventory>();

        inv.ChangeHealth(-damage);

        playerHit = false;
        bounceTimer = 0;
    }
}
