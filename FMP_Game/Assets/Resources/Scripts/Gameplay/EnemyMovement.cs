using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    EnemyAnimations anim;
    public float speed;
    public float runMultiplier;
    private float walkSpeed;
    private float runSpeed;
    public int jumpHeight;
    internal bool isJumping;

    private bool walking;
    private float gravity;
    void Start()
    {
        isJumping = false;
        Physics2D.IgnoreLayerCollision(11, 11);
        walking = true;
        anim = GetComponent<EnemyAnimations>();
        rb = GetComponent<Rigidbody2D>();
        runSpeed = speed * runMultiplier;
        walkSpeed = speed;
        gravity = 1.7f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyJump")
        {
            Jump();
        }
    }
    public void SetWalking(bool isWalking)
    {
        walking = isWalking;
    }
    public void Move(bool RightIfTrue)
    {
        if (walking)
        {
            anim.WalkAnim();
            speed = walkSpeed;
        }
        else
        {
            anim.RunAnim();
            speed = runSpeed;
        }

        if(RightIfTrue)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
    void Jump()
    {
        isJumping = true;
        rb.AddForce(new Vector2(0, jumpHeight));
        anim.JumpAnim();
    }
    public void StopHorizontal()
    {
        Vector2 stopHor = new Vector2(0, rb.velocity.y);
        rb.velocity = stopHor;
    }
    public void SetIgnorePlayer(GameObject player)
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<PolygonCollider2D>());
        GetComponent<EnemyAI>().SetPlayer(player);
    }
    public void ActivateGravity()
    {
        rb.gravityScale = gravity;
    }
}
