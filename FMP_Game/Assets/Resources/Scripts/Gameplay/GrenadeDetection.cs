using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDetection : MonoBehaviour
{
    public GameObject explosionFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Environment")
        {
            Explode();
        }
    }
    private void Explode()
    {
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);

        //explosion fx
        //radius detection
        //if enemy in radius, add force, kill enemy
        Destroy(this.gameObject);
        print("BANG");
    }
}
