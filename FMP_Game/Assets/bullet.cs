using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    bool isLeft;
    Rigidbody2D m_rb;
    private PlayerAnimations p_Anim;
    private PlayerAttacks p_Attacks;
    public float speed;

    private float killTimer;
    public float killTime = 1;

    void Start()
    {
        p_Anim = GameObject.Find("Player").GetComponent<PlayerAnimations>();
        p_Attacks = GameObject.Find("Player").GetComponent<PlayerAttacks>();
        m_rb = GetComponent<Rigidbody2D>();
        SetDirection(p_Anim.isFacingLeft);

        killTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            m_rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            m_rb.velocity = new Vector2(speed, 0);
        }

        killTimer += Time.deltaTime;

        if (killTimer > killTime)
        {
            RemoveBullet();
        }
    }
    private void RemoveBullet()
    {
        Destroy(gameObject);
        p_Attacks.ChangeBulletCount(-1);
    }
    public void SetDirection(bool LeftifTrue)
    {
        isLeft = LeftifTrue;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            RemoveBullet();
            collision.gameObject.GetComponent<EnemyHealth>().ApplyDamage(p_Attacks.GetGunDamage());
        }
        if (collision.tag == "Environment")
        {
            RemoveBullet();
        }
    }
}
