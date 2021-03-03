using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { darkorb, health, speed, damage };

    public PickupType pType;
    public Sprite[] icons;
    private SpriteRenderer icon;
    private SpriteRenderer highlight;
    private GameObject player;

    private int OrbValue;
    private int healthValue;
    private int speedMultiplier;
    private int damageMultiplier;

    public Color orbColor;
    public Color orbHighlight;
    public Color healthColor;
    public Color healthHighlight;
    public Color speedColor;
    public Color speedHighlight;
    public Color damageColor;
    public Color damageHighlight;

    private void Start()
    {
        OrbValue = 1;
        healthValue = 50;
        speedMultiplier = 2;
        damageMultiplier = 2;

        highlight = GetComponent<SpriteRenderer>();
        icon = transform.GetChild(0).GetComponent<SpriteRenderer>();
        AppearanceSwitch();
    }
    private void AppearanceSwitch()
    {
        switch (pType)
        {
            case PickupType.darkorb:
                icon.sprite = icons[0];
                icon.color = orbColor;
                icon.gameObject.transform.localScale = new Vector3(23, 23, 23);
                //highlight.color = orbHighlight;
                break;
            case PickupType.health:
                icon.sprite = icons[1];
                icon.color = healthColor;
                //highlight.color = healthHighlight;

                break;
            case PickupType.speed:
                icon.sprite = icons[2];
                icon.color = speedColor;
                //highlight.color = speedHighlight;

                break;
            case PickupType.damage:
                icon.sprite = icons[3];
                icon.color = damageColor;
                //highlight.color = damageHighlight;

                break;
            default:
                print("Error: No Pickup Type");
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            BehaviourSwitch();

            //dont destroy, start effect timer, disable collider, disable both sprite renderers
            Destroy(this.gameObject);
        }
    }
    private void BehaviourSwitch()
    {
        switch (pType)
        {
            case PickupType.darkorb:
                player.GetComponent<Inventory>().ChangeDarkOrbs(OrbValue);
                break;
            case PickupType.health:
                player.GetComponent<Inventory>().ChangeHealth(healthValue);
                break;
            case PickupType.speed:
                player.GetComponent<PlayerMovement>().speed *= speedMultiplier;
                break;
            case PickupType.damage:
                int damage = player.GetComponent<PlayerAttacks>().GetGunDamage();
                player.GetComponent<PlayerAttacks>().SetGunDamage(damage *= damageMultiplier);
                break;
            default:
                print("Error: No Pickup Type");
                break;
        }
    }
}
