using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;
    Vector2 left;
    Vector2 right;
    PlayerAnimations p_Anim;
    PlayerAttacks p_Attack;

    public float speed;
    public int jumpHeight;
    public Transform jumpFrom;
    private float maxSpeed;
    private float acceleration;

    public float rayLength;
    public LayerMask groundLayer;

    internal bool hasGun;
    private bool isCrouched;
    public Sprite crouchSprite;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        p_Anim = GetComponent<PlayerAnimations>();
        p_Attack = GetComponent<PlayerAttacks>();

       // speed = 1.25f;

       // jumpHeight = 245;

        maxSpeed = 1.25f;
        acceleration = 0.1f;

        rayLength = 0.5f;

        isCrouched = false;
        hasGun = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            p_Anim.SetIdle();
        }

        if (!isCrouched)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }


        if (Input.GetKey(KeyCode.S) && hasGun)
        {
            isCrouched = true;
            p_Anim.SetCrouched();
            p_Attack.SetCrouched(true);
        }
        else
        {
            isCrouched = false;
            p_Attack.SetCrouched(false);
        }
    }

    private void MoveLeft()
    {
        if (!isCrouched)
        {
            m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);
            p_Anim.SetMove();
        }
        p_Anim.isMovingLeft = true;
    }

    private void MoveRight()
    {
        if (!isCrouched)
        {
            m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
            p_Anim.SetMove();
        }
        p_Anim.isMovingLeft = false;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            m_rb.AddForce(new Vector2(0, jumpHeight));
            p_Anim.SetJumpAnim();
        }
    }
    private bool isGrounded()
    {
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(jumpFrom.position, direction, rayLength, groundLayer);
        Debug.DrawRay(jumpFrom.position, direction, Color.green, 10);

        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
