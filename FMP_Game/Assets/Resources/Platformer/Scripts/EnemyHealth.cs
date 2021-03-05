using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    private bool isDead;
    public bool ShouldExplode;
    private HitEffect hitEffect;
    public GameObject deathFXPrefab;
    private GameObject deathFX;
    private EnemyAnimations anim;
    private CapsuleCollider2D collider;
    private Rigidbody2D rb;
    public float deathYchange;
    private HUDManager hud;
    private EnemyAI ai;
    void Start()
    {
        isDead = false;
        hud = GameObject.Find("Canvas").GetComponent<HUDManager>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<EnemyAnimations>();
        health = 200;
        hitEffect = GetComponent<HitEffect>();
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (!isDead)
            {
                Kill();
                isDead = true;
            }
        }
    }

    public void ApplyDamage(int damage)
    {
        health += -damage;
        hitEffect.Enable();
        anim.Hit();
    }
    private void Kill()
    {
        if (ShouldExplode)
        {
            deathFX = Instantiate(deathFXPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            anim.Dead();
            collider.enabled = false;
            rb.gravityScale = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y - deathYchange, transform.position.z);
        }
        hud.IncreaseEnemiesKilled();
        ai.SetDead();
    }
}
