using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed;
    public LayerMask boundaryLayer;
    public Transform Upper;
    public Transform Lower;
    public float ColliderSize;

    private Transform currentTransform;
    private bool movingUp;
    private Rigidbody2D rb;
    private Vector2 upVelocity;
    private Vector2 downVelocity;
    void Start()
    {
        upVelocity = new Vector2(0, speed);
        rb = GetComponent<Rigidbody2D>();
        movingUp = true;
        currentTransform = Upper;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }
    private void MoveUp()
    {
        CheckMove();
        rb.velocity = upVelocity;
    }
    private void MoveDown()
    {
        CheckMove();
        rb.velocity = -upVelocity;
    }
    private void ChangeDirection()
    {
        movingUp = !movingUp;

        if (movingUp)
        {
            currentTransform = Upper;
        }
        else
        {
            currentTransform = Lower;
        }
    }
    private void CheckMove()
    {
        Collider2D tileDetection = Physics2D.OverlapCircle(currentTransform.position, ColliderSize, boundaryLayer);
        
        if(tileDetection != null && tileDetection.gameObject != this.gameObject)
        {
            ChangeDirection();
        }
    }
}
