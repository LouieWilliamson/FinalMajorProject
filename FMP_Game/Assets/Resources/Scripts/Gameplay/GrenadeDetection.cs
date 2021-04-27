using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDetection : MonoBehaviour
{
    public GameObject explosionFX;
    public float explosionRadius;
    private AudioManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
    }
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
        sound.PlaySFX(AudioManager.SFX.GrenadeExplosion);
        Destroy(this.gameObject);
    }
}
