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

        do
        {
            Collider2D tileDetection = Physics2D.OverlapCircle(transform.position, colliderSize, groundLayer);

            if (tileDetection != null)
            {
                onGround = true;
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
            }
        } while (!onGround);

    }
}
