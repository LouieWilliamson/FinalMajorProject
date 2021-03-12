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
        sr.sprite = UpgradeSprites[(int)type];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Inventory>().StoreUpgrade(type, sr.sprite);
            Destroy(this.gameObject);
        }
    }
}
