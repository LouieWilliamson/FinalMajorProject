using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public Upgrade type;
    public Sprite[] UpgradeSprites;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        switch (type)
        {
            case Upgrade.Cooldown:

                break;
            case Upgrade.Laser:

                break;
            case Upgrade.Grenade:

                break;
            default:
                print("Error: No Upgrade Type Selected");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Inventory>().StoreUpgrade(type);
            Destroy(this.gameObject);
        }
    }
}
