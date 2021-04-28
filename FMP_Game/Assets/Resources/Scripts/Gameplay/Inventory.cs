using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrade { None, Cooldown, Laser, Grenade };

public class Inventory : MonoBehaviour
{
    //Upgrades

    private Upgrade storedUpgrade;
    private Upgrade activeUpgrade;
    private bool inRangeOfUpgrade;
    private WeaponUpgrade upgradeInRange;
    private bool upgradeActive;
    private float UpgradeTimer;

    private int UpgradeSeconds;
    private PlayerAttacks pAttacks;

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
    private bool isDead;
    void Start()
    {
        isDead = false;
        UpgradeSeconds = 5;
        upgradeActive = false;
        UpgradeTimer = 0;
        storedUpgrade = Upgrade.None;
        activeUpgrade = Upgrade.None;
        inRangeOfUpgrade = false;

        gsManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GamestateManager>();

        anim = GetComponent<PlayerAnimations>();
        pAttacks = GetComponent<PlayerAttacks>();
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
        if (health <= 0 && !isDead)
        {
            Kill();
        }

        if (inRangeOfUpgrade && Input.GetKeyDown(KeyCode.E))
        {
            upgradeInRange.PickupUpgrade();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            UseUpgrade();
        }

        if (upgradeActive)
        {
            UpgradeTimer += Time.deltaTime;

            HUD.ChangeCounterOverlay(Time.deltaTime / 5);

            if (UpgradeTimer > 1)
            {
                UpgradeTimer = 0;
                UpgradeSeconds--;
                HUD.ChangeActiveCounter(UpgradeSeconds.ToString());

                if (UpgradeSeconds <= 0)
                {
                    DisableUpgrade();
                    HUD.ResetCounterOverlay();
                }
            }
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            pAttacks.sound.PlaySFX(AudioManager.SFX.HitPlayer);
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
        isDead = true;
        pAttacks.sound.PlaySFX(AudioManager.SFX.PlayerDeath);
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
            upgradeActive = true;
            activeUpgrade = storedUpgrade;
            storedUpgrade = Upgrade.None;
            pAttacks.activeUpgrade = activeUpgrade;
            HUD.UseStoredUpgrade();
            pAttacks.sound.PlaySFX(AudioManager.SFX.PickupUsed);
        }
    }
    private void DisableUpgrade()
    {
        upgradeActive = false;
        UpgradeSeconds = 5;
        UpgradeTimer = 0;

        activeUpgrade = Upgrade.None;
        pAttacks.activeUpgrade = activeUpgrade;
        HUD.DisableActiveUpgrade();
    }
    public void InUnpgradeRange(WeaponUpgrade upgrade)
    {
        inRangeOfUpgrade = true;
        upgradeInRange = upgrade;
    }
    public void OutofRange()
    {
        inRangeOfUpgrade = false;
        upgradeInRange = null;
    }
}
