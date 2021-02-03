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

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        p_Anim = GetComponent<PlayerAnimations>();

        speed = 1.25f;
        jumpHeight = 150;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MoveLeft()
    {
        m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);
        p_Anim.SetMove();
    }

    private void MoveRight()
    {
        // m_rb.AddForce(new Vector2(1, m_rb.velocity.y));
        m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
        p_Anim.SetMove();
    }

    private void Jump()
    {
        m_rb.AddForce(new Vector2(0, jumpHeight));
        p_Anim.SetJumpAnim();
    }
}
