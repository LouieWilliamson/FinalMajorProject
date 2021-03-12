using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrade { None, Cooldown, Laser, Grenade };

public class Inventory : MonoBehaviour
{
    //Upgrades

    private Upgrade storedUpgrade;
    private Upgrade activeUpgrade;

    // Start is called before the first frame update
    int DarkOrbs;
    public int baseHealth;
    public int maxHealth;
    public int healthMultiplier;
    int health;
    private HitEffect hitEffect;
    private PlayerAnimations anim;
    private HUDManager HUD;
    private GamestateManager gsManager;
    void Start()
    {
        storedUpgrade = Upgrade.None;
        activeUpgrade = Upgrade.None;

        gsManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GamestateManager>();

        anim = GetComponent<PlayerAnimations>();
        hitEffect = GetComponent<HitEffect>();
        HUD = GameObject.Find("Canvas").GetComponent<HUDManager>();
        health = baseHealth * healthMultiplier;
        maxHealth = health;
        DarkOrbs = 0;

        HUD.ChangeHealth(health);
        HUD.ChangeMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Kill();
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            hitEffect.Enable();
            anim.SetHit();
        }

        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        HUD.ChangeHealth(health);
    }
    public void ChangeMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        HUD.ChangeMaxHealth(maxHealth);
    }
    public void ChangeDarkOrbs(int amount)
    {
        DarkOrbs += amount;
        HUD.ChangeOrbCount(DarkOrbs);
    }
    private void Kill()
    {
        gsManager.GameOver();
    }
    public void StoreUpgrade(Upgrade newUpgrade, Sprite upgradeSprite)
    {
        storedUpgrade = newUpgrade;
        HUD.AddStoredUpgrade(upgradeSprite);
    }
    public void UseUpgrade()
    {
        if (storedUpgrade != Upgrade.None)
        {
            activeUpgrade = storedUpgrade;
            storedUpgrade = Upgrade.None;
            //activate effect
        }
    }
}
