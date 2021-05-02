using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType { darkorb, health, speed, damage };

public class Pickup : MonoBehaviour
{

    public PickupType pType;
    private PickupType currentType;

    public Sprite[] icons;
    private SpriteRenderer icon;
    private SpriteRenderer highlight;
    private GameObject player;

    private int OrbValue;
    private int healthValue;
    private float speedMultiplier;
    private float damageMultiplier;
    private float rawDamage;
    private float normalScale;

    private bool effectActive;
    private float effectTimer;
    private float effectDuration;

    public Color orbColor;
    public Color healthColor;
    public Color speedColor;
    public Color damageColor;

    private CircleCollider2D col;

    private PlayerMovement pMove;
    private PlayerAttacks pAttacks;

    private AudioManager sound;

    public GameObject darkOrbPrefab;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();

        currentType = pType;

        effectActive = false;
        effectTimer = 0;
        effectDuration = 2;

        rawDamage = 0;
        OrbValue = 1;
        healthValue = 50;
        speedMultiplier = 1.5f;
        damageMultiplier = 1.113f;
        normalScale = 0.6528f;

        highlight = GetComponent<SpriteRenderer>();
        icon = transform.GetChild(0).GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        AppearanceSwitch();

    }
    private void Update()
    {
        if (currentType != pType)
        {
            AppearanceSwitch();
            currentType = pType;
        }

        if (effectActive)
        {
            effectTimer += Time.deltaTime;

            if (effectTimer >= effectDuration)
            {
                DisableEffect();
                effectTimer = 0;
            }
        }
    }
    private void AppearanceSwitch()
    {
        switch (pType)
        {
            case PickupType.darkorb:
                Instantiate(darkOrbPrefab, transform.position, transform.rotation);
                Destroy(this.gameObject);
                break;
            case PickupType.health:
                icon.sprite = icons[1];
                icon.color = healthColor;
                icon.gameObject.transform.localScale = new Vector3(normalScale, normalScale, normalScale);
                //highlight.color = healthHighlight;

                break;
            case PickupType.speed:
                icon.sprite = icons[2];
                icon.color = speedColor;
                icon.gameObject.transform.localScale = new Vector3(normalScale, normalScale, normalScale);
                //highlight.color = speedHighlight;

                break;
            case PickupType.damage:
                icon.sprite = icons[3];
                icon.color = damageColor;
                icon.gameObject.transform.localScale = new Vector3(normalScale, normalScale, normalScale);
                //highlight.color = damageHighlight;

                break;
            default:
                print("Error: No Pickup Type");
                break;
        }
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            BehaviourSwitch();
            sound.PlaySFX(AudioManager.SFX.CollectItem);

            //if it doesnt have a timed effect, then destroy it
            if (pType == PickupType.health || pType == PickupType.darkorb)
            {
                Destroy(this.gameObject);
            }
            else
            {
                icon.enabled = false;
                highlight.enabled = false;
                col.enabled = false;
            }
        }
    }
    private void BehaviourSwitch()
    {
        Inventory inv = player.GetComponent<Inventory>();
        if (inv == null) inv = player.GetComponentInParent<Inventory>();

        switch (pType)
        {
            case PickupType.darkorb:
                inv.ChangeDarkOrbs(OrbValue);
                break;
            case PickupType.health:
                inv.ChangeHealth(healthValue);
                break;
            case PickupType.speed:
                print("Speed Enabled");
                pMove = player.GetComponent<PlayerMovement>();
                if (pMove == null) pMove = player.GetComponentInParent<PlayerMovement>();

                pMove.speed *= speedMultiplier;
                effectActive = true;
                break;
            case PickupType.damage:
                pAttacks = player.GetComponent<PlayerAttacks>();
                if (pAttacks == null) pAttacks = player.GetComponentInParent<PlayerAttacks>();

                float damage = pAttacks.GetGunDamage();
                rawDamage = damage *= damageMultiplier;
                pAttacks.SetGunDamage((int)Mathf.Round(rawDamage));
                effectActive = true;
                break;
            default:
                print("Error: No Pickup Type");
                break;
        }
    }
    private void DisableEffect()
    {
        if(pType == PickupType.speed)
        {
            pMove.speed /= speedMultiplier;
            print("Speed Disabled");
        }
        else if (pType == PickupType.damage)
        {
            pAttacks.SetGunDamage((int)Mathf.Round(rawDamage /= damageMultiplier));
        }
        effectActive = false;
        Destroy(this.gameObject);
    }
}
