using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HorizontalPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public LayerMask boundaryLayer;
    public Transform RightA;
    public Transform RightB;

    public Transform LeftA;
    public Transform LeftB;

    public float ColliderSize;

    private Transform currentTransformA;
    private Transform currentTransformB;

    private bool movingRight;

    private Rigidbody2D rb;
    private Vector2 rightVelocity;

    private bool isActive;
    private bool playerInRange;

    public Light2D leftLight;
    public Light2D rightLight;

    private PlayerMovement pMove;
    private PlayerTooltip pTooltip;

    void Start()
    {
        playerInRange = false;
        isActive = true;
        rightVelocity = new Vector2(speed, 0);
        rb = GetComponent<Rigidbody2D>();
        movingRight = true;
        currentTransformA = RightA;
        currentTransformB = RightB;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isActive = !isActive;
            leftLight.enabled = isActive;
            rightLight.enabled = isActive;
        }

        if (isActive)
        {
            if (movingRight)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                ZeroVelocity();
            }
        }
    }
    private void MoveRight()
    {
        CheckMove();
        rb.velocity = rightVelocity;
    }
    private void MoveLeft()
    {
        CheckMove();
        rb.velocity = -rightVelocity;
    }
    private void ZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    private void ChangeDirection()
    {
        movingRight = !movingRight;

        if (movingRight)
        {
            currentTransformA = RightA;
            currentTransformB = RightB;
        }
        else
        {
            currentTransformA = LeftA;
            currentTransformB = LeftB;
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
            GetPlayerReferences(collision);
            pMove.SetMovingPlatform(true, rb);

            if (collision.name.Contains("Player")) pTooltip.SetTipText(PlayerTooltip.TipType.XPlatform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
            pMove.SetMovingPlatform(false, rb);
        }
    }
    private void GetPlayerReferences(Collider2D player)
    {
        if (pMove == null)
        {
            if (player.GetComponent<PlayerMovement>() == null)
            {
                pMove = player.GetComponentInParent<PlayerMovement>();
            }
            else
            {
                pMove = player.GetComponent<PlayerMovement>();
            }
        }
        if (pTooltip == null)
        {
            if (player.GetComponentInChildren<PlayerTooltip>() == null)
            {
                pTooltip = player.transform.parent.GetComponentInChildren<PlayerTooltip>();
            }
            else
            {
                pTooltip = player.GetComponentInChildren<PlayerTooltip>();
            }
        }
    }
}
