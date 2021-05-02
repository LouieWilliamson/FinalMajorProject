using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    internal bool isDead;
    public bool ShouldExplode;
    private HitEffect hitEffect;
    public GameObject deathFXPrefab;
    private GameObject deathFX;
    private EnemyAnimations anim;
    private EnemyMovement eMove;
    private PolygonCollider2D col;
    private Rigidbody2D rb;
    public float deathYchange;
    private HUDManager hud;
    private EnemyAI ai;
    private AudioManager sound;

    public LayerMask platformLayer;
    public Transform deadDetection;
    public Light2D enemyLight;
    private SpriteRenderer sRend;
    void Start()
    {
        sRend = GetComponent<SpriteRenderer>();
        isDead = false;
        hud = GameObject.Find("Canvas").GetComponent<HUDManager>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        anim = GetComponent<EnemyAnimations>();
        eMove = GetComponent<EnemyMovement>();

        health = 200;
        hitEffect = GetComponent<HitEffect>();
        ai = GetComponent<EnemyAI>();
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
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
        if (isDead)
        {
            CheckIfOnPlatform();
        }
    }
    private void CheckIfOnPlatform()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(deadDetection.position, 1, platformLayer);
        
        bool onPlatform = roomDetection != null;

        if (onPlatform)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), roomDetection);
        }
    }
    public void ApplyDamage(int damage)
    {
        if (!isDead)
        {
            health += -damage;
            hitEffect.Enable();
            anim.HitAnim();
            sound.PlaySFX(AudioManager.SFX.HitEnemy);
        }
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
            rb.AddForce(new Vector2(0, 20));
            anim.DeadAnim();
            enemyLight.enabled = false;
            sRend.sortingLayerName = "Environment";
            sRend.sortingOrder = 6;
        }
        eMove.isDead = true;
        sound.PlaySFX(AudioManager.SFX.EnemyDeath);
        hud.IncreaseEnemiesKilled();
        ai.SetDead();
    }
}
