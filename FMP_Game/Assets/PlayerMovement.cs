using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;
    Vector2 left;
    Vector2 right;
    PlayerAnimations p_Anim;

    public float speed;
    public int jumpHeight;

    public float maxSpeed;
    public float acceleration;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        p_Anim = GetComponent<PlayerAnimations>();

        speed = 0;// 1.25f;

        jumpHeight = 150;

        maxSpeed = 1.25f;
        acceleration = 0.1f;
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
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MoveLeft()
    {
        IncreaseSpeed();
        m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);
        p_Anim.SetMove();
    }

    private void MoveRight()
    {
        IncreaseSpeed();
        // m_rb.AddForce(new Vector2(1, m_rb.velocity.y));
        m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
        p_Anim.SetMove();
    }

    private void Jump()
    {
        //m_rb.AddForce(new Vector2(0, jumpHeight));
        p_Anim.SetJumpAnim();
    }

    private void IncreaseSpeed()
    {
        speed += acceleration;

        if (speed > 1.25f) speed = 1.25f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            p_Anim.SetGun(true);
            Destroy(collision.gameObject);
        }
    }
}
