using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Anim;
    Rigidbody2D m_rb;

    internal bool isFacingLeft;
    internal bool isMovingLeft;
    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = Vector2.zero;
        isFacingLeft = false;
        isMovingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingLeft)
        {
            if (!isFacingLeft)
            {
                FlipSprite();
                isFacingLeft = true;
            }
        }
        else
        {
            if (isFacingLeft)
            {
                FlipSprite();
                isFacingLeft = false;
            }
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void SetGun(bool hasGun)
    {
        m_Anim.SetBool("hasGun", hasGun);
    }

    public void SetJumpAnim()
    {
        m_Anim.SetTrigger("isJumping");
    }
    public void SetDead()
    {
        m_Anim.SetTrigger("isDead");
    }
    public void SetMove()
    {
        m_Anim.SetTrigger("isMoving");
    }
    public void SetHit()
    {
        m_Anim.SetTrigger("isHit");
    }
    public void SetIdle()
    {
        m_Anim.SetTrigger("isIdle");
    }
    public void ClearTrigger(string triggerName)
    {
        m_Anim.ResetTrigger(triggerName);
    }
    public void SetCrouched(bool isCrouched)
    {
        m_Anim.SetBool("isCrouched", isCrouched);
    }
}
