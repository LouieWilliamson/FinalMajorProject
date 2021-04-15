using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    bool isFacingLeft;
    Rigidbody2D m_rb;
    Animator m_Anim;
    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        isFacingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Is he moving to the left
        if (m_rb.velocity.x < 0)
        {
            //is he already facing left?
            if (!isFacingLeft)
            {
                FlipEnemy();
                //isFacingLeft = true;
            }
        }
        else if (m_rb.velocity.x > 0)
        {
            if (isFacingLeft)
            {
                FlipEnemy();
                //isFacingLeft = false;
            }
        }
        else if(m_rb.velocity.x == 0)
        {
            IdleAnim();
        }
    }
    public void FlipEnemy()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        isFacingLeft = !isFacingLeft;
    }

    public void IdleAnim()
    {
        m_Anim.SetTrigger("isIdle");
    }
    public void WalkAnim()
    {
        m_Anim.SetTrigger("isWalking");
    }
    public void RunAnim()
    {
        m_Anim.SetTrigger("isRunning");
    }
    public void JumpAnim()
    {
        m_Anim.SetTrigger("isJumping");
    }
    public void HitAnim()
    {
        m_Anim.SetTrigger("isHit");
    }
    public void DeadAnim()
    {
        m_Anim.SetBool("isDead", true);

    }
    public void LightAttackAnim()
    {
        m_Anim.SetTrigger("LightAttack");
    }
    public void HeavyAttackAnim()
    {
        m_Anim.SetTrigger("HeavyAttack");
    }
    public bool GetFacingLeft() { return isFacingLeft; }
}
