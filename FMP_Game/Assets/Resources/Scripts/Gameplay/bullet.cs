using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    bool isLeft;
    Rigidbody2D m_rb;
    private GameObject player;
    private PlayerAnimations p_Anim;
    private PlayerAttacks p_Attacks;
    public float speed;

    private float killTimer;
    public float killTime = 1;

    public GameObject impactPrefab;
    private GameObject impact;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        p_Anim = player.GetComponent<PlayerAnimations>();
        p_Attacks = player.GetComponent<PlayerAttacks>();
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
        impact = Instantiate(impactPrefab, transform.position, transform.rotation);
    }
    public void SetDirection(bool LeftifTrue)
    {
        isLeft = LeftifTrue;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!collision.GetComponent<EnemyHealth>().isDead)
            {
                RemoveBullet();
                collision.gameObject.GetComponent<EnemyHealth>().ApplyDamage(p_Attacks.GetGunDamage());
            }
        }
        if (collision.tag == "Environment")
        {
            p_Attacks.sound.PlaySFX(AudioManager.SFX.HitWall);
            RemoveBullet();
        }
    }
}
