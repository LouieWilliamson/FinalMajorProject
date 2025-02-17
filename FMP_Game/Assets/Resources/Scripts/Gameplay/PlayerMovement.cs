﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;
    PlayerAnimations p_Anim;
    PlayerAttacks p_Attack;

    internal bool playerActive;

    public float speed;
    public int jumpHeight;
    public Transform jumpFrom;

    public float rayLength;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    internal bool hasGun;
    internal bool isCrouched;
    public Sprite crouchSprite;
    private int TimesJumped;

    bool isAPressed;
    bool isSPressed;
    bool isDPressed;
    bool isSpacePressed;

    bool beenHit;
    float hitTimer;

    CapsuleCollider2D capsuleCol;
    BoxCollider2D boxCol;
    WallFace wallFace;

    HUDManager hud;

    public Transform crouchCheck;
    public BoxCollider2D crouchCollider;
    public LayerMask enemyLayer;

    private bool isOnMovingPlatform;
    private Rigidbody2D platformRB;
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.Find("Canvas").GetComponent<HUDManager>();
        playerActive = true;

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

        capsuleCol = GetComponent<CapsuleCollider2D>();
        boxCol = GetComponentInChildren<BoxCollider2D>();
        wallFace = GameObject.Find("Guardian").GetComponent<WallFace>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerActive && !hud.isPaused) GetInput();

        //if the player has the gun, presses "S" and isn't crouched already then crouch
        if (isSPressed && hasGun && !isCrouched)
        {
            isCrouched = true;
            p_Anim.SetCrouched(true);
            p_Attack.SetCrouched(true);

            if(wallFace.playerTeleported)
            {
                boxCol.enabled = true;
                capsuleCol.enabled = false;
            }
        }
        //else if the player is crouched (and they're not pressing "S")
        else if (isCrouched && !isSPressed)
        {
            isCrouched = false;
            p_Attack.SetCrouched(false);
            p_Anim.SetCrouched(false);

            if(wallFace.playerTeleported)
            {
                capsuleCol.enabled = true;
                boxCol.enabled = false;
            }
        }

        if (!isCrouched)
        {
            if (isSpacePressed)
            {
                Jump();
            }
        }
        else
        {
            p_Anim.ClearTrigger("isHit");
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

        if (isCrouched)
        {
            CheckOnEnemy();
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

                if (!isOnMovingPlatform)
                {
                    StopHorizontal();
                }
                else
                {
                    m_rb.velocity = new Vector2(platformRB.velocity.x, m_rb.velocity.y);
                }
            }
        }
    }
    public void SetMovingPlatform(bool onPlatform, Rigidbody2D pformRB) 
    {
        if (onPlatform)
        {
            if (platformRB == null) platformRB = pformRB;
        }
        else
        {
            platformRB = null;
        }
        isOnMovingPlatform = onPlatform; 
    }
    private void MoveLeft()
    {
        if (!isCrouched)
        {
            if (!isOnMovingPlatform)
            {
                m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);
            }
            else
            {
                float velocityX = -speed + platformRB.velocity.x;
                m_rb.velocity = new Vector2(velocityX, m_rb.velocity.y);
            }
            p_Anim.SetMove();
        }
        p_Anim.isMovingLeft = true;
    }

    private void MoveRight()
    {
        if (!isCrouched)
        {
            if (!isOnMovingPlatform)
            {
                m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
            }
            else
            {
                float velocityX = speed + platformRB.velocity.x;
                m_rb.velocity = new Vector2(velocityX, m_rb.velocity.y);
            }

            p_Anim.SetMove();
        }
        p_Anim.isMovingLeft = false;
    }

    private void Jump()
    {
        if (isGrounded() || TimesJumped < 2)
        {
            p_Attack.sound.PlaySFX(AudioManager.SFX.Jump);
            m_rb.velocity = Vector2.up * jumpHeight;
            p_Anim.SetJumpAnim();
            TimesJumped++;
        }

    }
    public void StopHorizontal()
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
    public void ResetInput()
    {
        isAPressed = false;
        isSPressed = false;
        isDPressed = false;
        isSpacePressed = false;
    }
    private void CheckOnEnemy()
    {
        Collider2D enemyDetection = Physics2D.OverlapCircle(crouchCheck.position, 1, enemyLayer);

        if (enemyDetection != null)
        {
            Physics2D.IgnoreCollision(crouchCollider, enemyDetection);
        }
    }
    private bool isGrounded()
    {
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(jumpFrom.position, direction, rayLength, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(jumpFrom.position, direction, rayLength, platformLayer);
        
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
