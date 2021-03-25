using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform EndOfGun;

    public Transform normalGun;
    public Transform crouchGun;
    private bool isCrouched;

    public GameObject bulletPrefab;
    private GameObject bullet;
    PlayerAnimations p_Anim;
    PlayerMovement p_Mvmt;
    private bool hasGun;
    private int bulletCount;
    public int maxBullets;
    private int GunDamage;
    private AudioManager sound;

    //upgrades
    public Upgrade activeUpgrade;
    public Laser laser;

    void Start()
    {
        activeUpgrade = Upgrade.Laser;
        
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
        isCrouched = false;
        bulletCount = 0;
        maxBullets = 3;

        hasGun = false;
        p_Anim = GetComponent<PlayerAnimations>();
        p_Mvmt = GetComponent<PlayerMovement>();
        GunDamage = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                switch (activeUpgrade)
                {
                    case Upgrade.None:
                        if (hasGun && bulletCount < maxBullets) Shoot();
                        break;
                    case Upgrade.Cooldown:
                        //disable cooldown
                        break;
                    case Upgrade.Laser:
                        laser.ActivateLaser();
                        break;
                    case Upgrade.Grenade:
                        //shoot grenade
                        break;
                    default:
                        break;
                }
            }
            if (activeUpgrade == Upgrade.Laser)
            {
                if (Input.GetButton("Fire1"))
                {
                    laser.UpdateLaser();
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    laser.DeactivateLaser();
                }
            }
        }
    }

    private void Shoot()
    {
        if(isCrouched)
        {
            EndOfGun = crouchGun;
        }
        else
        {
            EndOfGun = normalGun;
        }
        bullet = Instantiate(bulletPrefab, EndOfGun.position, EndOfGun.rotation);
        bullet.GetComponent<bullet>().SetDirection(!p_Anim.isFacingLeft);
        sound.PlaySFX(AudioManager.SFX.Shoot);
        ChangeBulletCount(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            p_Anim.SetGun(true);
            Destroy(collision.gameObject);
            hasGun = true;
            p_Mvmt.hasGun = true;
        }
    }
    public void SetCrouched(bool trueIfCrouched)
    {
        isCrouched = trueIfCrouched;
    }
    public void ChangeBulletCount(int change)
    {
        bulletCount += change;
    }
    public void SetGunDamage(int newDamage)
    {
        GunDamage = newDamage;
    }
    public int GetGunDamage()
    {
        return GunDamage;
    }
    public Upgrade GetActiveUpgrade() { return activeUpgrade; }
    public void SetActiveUpgrade(Upgrade newUpgrade) { activeUpgrade = newUpgrade; }
}
