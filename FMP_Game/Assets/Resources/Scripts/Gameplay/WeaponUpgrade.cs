using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public Upgrade type;
    public Sprite[] UpgradeSprites;

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
        UpdateScale();

        Physics2D.IgnoreLayerCollision(13, 11); //upgrade to enemy
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
                playerSet = true;
            }
            inv.InUnpgradeRange(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inv.OutofRange();
        }
    }
    private void UpdateScale()
    {
        float scale = 1.756f;

        switch (type)
        {
            case Upgrade.Cooldown:
                scale = 4.45f;
                break;
            case Upgrade.Laser:
                scale = 4.976f;
                break;
            case Upgrade.Grenade:
                scale = 1.756f;
                break;
        }
        transform.GetChild(0).transform.localScale = new Vector3 (scale, scale, scale);
    }
}
