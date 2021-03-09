using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;
    PlayerAnimations p_Anim;
    PlayerAttacks p_Attack;

    public float speed;
    public int jumpHeight;
    public Transform jumpFrom;

    public float rayLength;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    internal bool hasGun;
    private bool isCrouched;
    public Sprite crouchSprite;
    private int TimesJumped;

    bool isAPressed;
    bool isSPressed;
    bool isDPressed;
    bool isSpacePressed;

    bool beenHit;
    float hitTimer;
    // Start is called before the first frame update
    void Start()
    {
        hitTimer = 0;
        beenHit = false;
        TimesJumped = 0;
        isAPressed = false;
        isSPressed = false;
        isDPressed = false;
        isSpacePressed = false;

        m_rb = GetComponent<Rigidbody2D>();
        p_Anim = GetComponent<PlayerAnimations>();
        p_Attack = GetComponent<PlayerAttacks>();

        isCrouched = false;
        hasGun = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        if (isSPressed && hasGun)
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

        if (!isCrouched)
        {
            if (isSpacePressed)
            {
                Jump();
            }
        }

        if (beenHit)
        {
            hitTimer += Time.deltaTime;

            if(hitTimer > 1)
            {
                beenHit = false;
                hitTimer = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDPressed)
        {
            MoveRight();
        }
        else if (isAPressed)
        {
            MoveLeft();
        }
        else if ((!isDPressed && !isAPressed) || (isDPressed && isAPressed))
        {
            if(!beenHit)
            {
                p_Anim.SetIdle();
                StopHorizontal();
            }
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
        if (isGrounded() || TimesJumped < 2)
        {
            m_rb.velocity = Vector2.up * jumpHeight;
            p_Anim.SetJumpAnim();
            TimesJumped++;
        }

    }
    private void StopHorizontal()
    {
        Vector2 stopHor = new Vector2( 0, m_rb.velocity.y);
        m_rb.velocity = stopHor;
    }
    private void GetInput()
    {
        isAPressed = Input.GetKey(KeyCode.A);
        isSPressed = Input.GetKey(KeyCode.S);
        isDPressed = Input.GetKey(KeyCode.D);
        isSpacePressed = Input.GetKeyDown(KeyCode.Space);
    }
    private bool isGrounded()
    {
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(jumpFrom.position, direction, rayLength, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(jumpFrom.position, direction, rayLength, platformLayer);
        
        Debug.DrawRay(jumpFrom.position, direction, Color.green, 10);

        if (hit.collider != null || hit2.collider != null)
        {
            TimesJumped = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Knockback(float strength, bool left)
    {
        float knockX = strength;
        float knockY = 5;

        if(left)
        {
            knockX *= -1;
        }

        beenHit = true;

        Vector2 knock = new Vector2(knockX, knockY);

        m_rb.AddForce(knock, ForceMode2D.Impulse);
    }
}
