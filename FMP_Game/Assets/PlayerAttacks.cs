using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform EndOfGun;
    public GameObject bulletPrefab;
    private GameObject bullet;
    PlayerAnimations p_Anim;
    private bool hasGun;
    private int bulletCount;
    public int maxBullets;
    void Start()
    {
        bulletCount = 0;
        maxBullets = 3;

        hasGun = false;
        p_Anim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(hasGun && bulletCount < maxBullets) Shoot();
        }
    }

    private void Shoot()
    {
        bullet = Instantiate(bulletPrefab, EndOfGun.position, EndOfGun.rotation);
        bullet.GetComponent<bullet>().SetDirection(!p_Anim.isFacingLeft);
        ChangeBulletCount(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            p_Anim.SetGun(true);
            Destroy(collision.gameObject);
            hasGun = true;
        }
    }

    public void ChangeBulletCount(int change)
    {
        bulletCount += change;
    }
}
