using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOrb : MonoBehaviour
{
    public int OrbValue;
    private AudioManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sound.PlaySFX(AudioManager.SFX.CollectItem);
            Inventory inv = collision.gameObject.GetComponent<Inventory>();
            if (inv == null) inv = collision.gameObject.GetComponentInParent<Inventory>();

            inv.ChangeDarkOrbs(OrbValue);
            Destroy(this.gameObject);
        }
    }
}
