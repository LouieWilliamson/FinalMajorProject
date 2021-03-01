using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask groundLayer;
    public float colliderSize;
    private bool onGround;
    void Start()
    {
        onGround = false;

        //move the platform down until it hits the floor
        do
        {
            Collider2D tileDetection = Physics2D.OverlapCircle(transform.position, colliderSize, groundLayer);

            if (tileDetection != null)
            {
                onGround = true;
                //this makes it 1 below the floor so its not too high
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
            }
        } while (!onGround);

    }
}
