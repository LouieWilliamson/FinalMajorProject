using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public Upgrade type;
    public Sprite[] UpgradeSprites;

    private PlayerTooltip pTooltip;

    private SpriteRenderer sr;
    private Inventory inv;
    private bool playerSet;
    private AudioManager sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
        playerSet = false;
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr.sprite = UpgradeSprites[(int)type]; //0 None, 1 Cooldown, 2 Laser, 3 Grenade 
    }
    public void PickupUpgrade()
    {
        sound.PlaySFX(AudioManager.SFX.CollectItem);
        inv.StoreUpgrade(type, sr.sprite);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!playerSet)
            {
                inv = collision.gameObject.GetComponent<Inventory>();
                if (inv == null) inv = collision.gameObject.GetComponentInParent<Inventory>();
                GetPlayerTooltip(collision);
                playerSet = true;
            }
            inv.InUnpgradeRange(this);
            pTooltip.SetTipText(PlayerTooltip.TipType.Upgrade);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inv.OutofRange();
        }
    }
    private void GetPlayerTooltip(Collider2D player)
    {
        if (pTooltip == null)
        {
            if (player.GetComponentInChildren<PlayerTooltip>() == null)
            {
                player.transform.parent.GetComponentInChildren<PlayerTooltip>();
            }
            else
            {
                pTooltip = player.GetComponentInChildren<PlayerTooltip>();
            }
        }
    }
}
