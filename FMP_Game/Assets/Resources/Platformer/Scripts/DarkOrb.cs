using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOrb : MonoBehaviour
{
    public int OrbValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Inventory inv = collision.gameObject.GetComponent<Inventory>();
            inv.ChangeDarkOrbs(OrbValue);
            Destroy(this.gameObject);
        }
    }
}
