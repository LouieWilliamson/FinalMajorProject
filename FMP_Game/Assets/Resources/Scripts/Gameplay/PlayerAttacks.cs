using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform EndOfGun;
    private bool isCrouched;

    public GameObject bulletPrefab;
    private GameObject bullet;
    PlayerAnimations p_Anim;
    PlayerMovement p_Mvmt;
    private bool hasGun;
    private int GunDamage;
    private AudioManager sound;

    private bool gunOverheated;
    private float overheatPercent;
    public float cooldownAmount;
    private bool isCooldownActive;

    //upgrades
    public Upgrade activeUpgrade;
    public Laser laser;
    private Grenade grenade;

    private HUDManager hud;
    void Start()
    {
        hud = GameObject.Find("Canvas").GetComponent<HUDManager>();
        //activeUpgrade = Upgrade.Grenade;

        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
        isCrouched = false;

        gunOverheated = false;
        overheatPercent = 0;
        isCooldownActive = true;

        hasGun = false;
        p_Anim = GetComponent<PlayerAnimations>();
        p_Mvmt = GetComponent<PlayerMovement>();
        grenade = GetComponent<Grenade>();
        GunDamage = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeUpgrade == Upgrade.Cooldown && isCooldownActive)
        {
            isCooldownActive = false;
            gunOverheated = false;
            overheatPercent = 0;
            print("cooldown deactivated");
        }
        if (activeUpgrade == Upgrade.None && !isCooldownActive)
        {
            isCooldownActive = true;
            print("cooldown activated");
        }

        if (!hud.isPaused)
        {
            if (hasGun && p_Mvmt.playerActive)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    switch (activeUpgrade)
                    {
                        case Upgrade.None:
                            Shoot();
                            break;
                        case Upgrade.Cooldown:
                            Shoot();
                            break;
                        case Upgrade.Laser:
                            laser.ActivateLaser();
                            break;
                        case Upgrade.Grenade:
                            grenade.Fire(p_Anim.isFacingLeft);
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
                else if (laser.lineRenderer.enabled)
                {
                    laser.DeactivateLaser();
                }
                //print(overheatPercent);
            }

            if (overheatPercent > 0)
            {
                overheatPercent -= cooldownAmount * Time.deltaTime;
            }
            else if (overheatPercent < 0)
            {
                //overheatPercent = 0;
            }
        }
    }

    private void Shoot()
    {
        CheckCooldown();
        
        //if (gunOverheated) do the code below
        if (!gunOverheated)
        {
            bullet = Instantiate(bulletPrefab, EndOfGun.position, EndOfGun.rotation);
            bullet.GetComponent<bullet>().SetDirection(!p_Anim.isFacingLeft);
            sound.PlaySFX(AudioManager.SFX.Shoot);

            if(isCooldownActive) overheatPercent += 33f;
        }
    }
    private void CheckCooldown()
    {
        if (overheatPercent >= 100)
        {
            overheatPercent = 100;
            gunOverheated = true;
        }

        if (gunOverheated && overheatPercent <= 0)
        {
            overheatPercent = 0;
            gunOverheated = false;
        }
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
