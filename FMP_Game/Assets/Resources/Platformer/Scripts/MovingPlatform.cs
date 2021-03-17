using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed;
    public LayerMask boundaryLayer;
    public Transform UpperA;
    public Transform UpperB;

    public Transform LowerA;
    public Transform LowerB;

    public float ColliderSize;

    private Transform currentTransformA;
    private Transform currentTransformB;

    private bool movingUp;
    private Rigidbody2D rb;
    private Vector2 upVelocity;
    private bool isActive;
    private bool playerInRange;
    void Start()
    {
        playerInRange = false;
        isActive = true;
        upVelocity = new Vector2(0, speed);
        rb = GetComponent<Rigidbody2D>();
        movingUp = true;
        currentTransformA = UpperA;
        currentTransformB = UpperB;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isActive = !isActive;
        }

        if (isActive)
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
        else
        {
            if (rb.velocity.y != 0)
            {
                ZeroVelocity();
            }
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
    private void ZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    private void ChangeDirection()
    {
        movingUp = !movingUp;

        if (movingUp)
        {
            currentTransformA = UpperA;
            currentTransformB = UpperB;
        }
        else
        {
            currentTransformA = LowerA;
            currentTransformB = LowerB;
        }
    }
    private void CheckMove()
    {
        Collider2D tileDetectionA = Physics2D.OverlapCircle(currentTransformA.position, ColliderSize, boundaryLayer);
        Collider2D tileDetectionB = Physics2D.OverlapCircle(currentTransformB.position, ColliderSize, boundaryLayer);

        bool hitBoundaryA = (tileDetectionA != null && tileDetectionA.gameObject != this.gameObject);
        bool hitBoundaryB = (tileDetectionB != null && tileDetectionB.gameObject != this.gameObject);

        if (hitBoundaryA || hitBoundaryB)
        {
            ChangeDirection();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
