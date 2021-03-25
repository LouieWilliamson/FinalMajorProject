using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //Health
    public GameObject HUD;
    public Slider HealthBar;
    public Text HealthText;
    private int maxHealth;
    private int Health;
    
    //Run Timer
    public Text RunTime;
    private float RunTimer;
    private bool isRunning;
    private int SS;
    private int MM;
    private int HH;

    //Dark Orb Counter
    public Text DarkOrbCount;

    //Enemy Kill Counter
    public Text EnemyPercentTxt;
    public Slider EnemyCountSlider;
    private int NumberOfEnemies;
    private int EnemiesKilled;
    private float enemyPercent;
    private bool EnemiesCounted;

    //Inventory
    public Image storedImage;
    public Image activeImage;
    private bool hasUpgrade;
    private bool hasActiveUpgrade;

    private bool LevelLoaded;

    private GamestateManager gsManager;

    public GameObject LoadingScreen;

    void Start()
    {
        EnemiesKilled = 0;
        enemyPercent = 0;
        LevelLoaded = false;
        EnemiesCounted = false;

        SS = 0;
        MM = 0;
        HH = 0;

        isRunning = false; //start this after player is spawned
        ChangeOrbCount(0);

        gsManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GamestateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelLoaded && !EnemiesCounted)
        {
            CountEnemies();
        }
        if (isRunning)
        {
            AddtoTimer();
            BuildTimerText();
        }
    }
    private void AddtoTimer()
    {
        RunTimer += Time.deltaTime;

        if (RunTimer >= 1)
        {
            SS++;
            RunTimer = 0;
        }
        if (SS == 60)
        {
            SS = 0; 
            MM++;
        }
        if(MM == 60)
        {
            MM = 0;
            HH++;
        }
    }
    private void BuildTimerText()
    {
        string Seconds, Minutes, Hours;

        if (SS >= 10)
        {
            Seconds = "" + SS;
        }
        else
        {
            Seconds = "0" + SS;
        }

        if (MM >= 10)
        {
            Minutes = "" + MM;
        }
        else
        {
            Minutes = "0" + MM;
        }

        if (HH >= 10)
        {
            Hours = "" + HH;
        }
        else
        {
            Hours = "0" + HH;
        }
        RunTime.text = Hours + ":" + Minutes + ":" + Seconds;
    }
    public void ChangeMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;

        HealthBar.maxValue = maxHealth;
        HealthText.text = Health + " / " + maxHealth;
    }
    public void ChangeHealth(int newHealth)
    {
        Health = newHealth;

        if (Health < 0) Health = 0;

        HealthBar.value = Health;
        HealthText.text = Health + " / " + maxHealth;
    }
    public void ChangeOrbCount(int OrbCount)
    {
        DarkOrbCount.text = "x " + OrbCount;
    }
    public void SetRunning(bool running)
    {
        isRunning = running;
    }
    public void SetLevelLoaded()
    {
        LevelLoaded = true;
        LoadingScreen.SetActive(false);
    }
    public void EnableHUD() { HUD.SetActive(true); }
    public void IncreaseEnemiesKilled()
    {
        EnemiesKilled++;
        CalculateEnemyPercent();
    }
    private void CountEnemies()
    {
        GameObject[] enemies;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            NumberOfEnemies++;
        }

        EnemiesCounted = true;

        CalculateEnemyPercent();
    }
    private void CalculateEnemyPercent()
    {
        if(NumberOfEnemies > 0)
        {
            enemyPercent = ((float)EnemiesKilled / (float)NumberOfEnemies) * 100;
            enemyPercent = Mathf.Round(enemyPercent);

            EnemyPercentTxt.text = enemyPercent + " %";
            EnemyCountSlider.value = enemyPercent;
        }
        else
        {
            print("Error: No Enemies Found");
        }
        if(enemyPercent == 100)
        {
            gsManager.GameWon();
        }
    }
    public float GetEnemyPercent() { return enemyPercent; }

    public void AddStoredUpgrade(Sprite upgradeSprite)
    {
        storedImage.sprite = upgradeSprite;
    }
    public void UseStoredUpgrade()
    {
        activeImage.sprite = storedImage.sprite;
        storedImage.sprite = null;
        //start timer
    }
}
