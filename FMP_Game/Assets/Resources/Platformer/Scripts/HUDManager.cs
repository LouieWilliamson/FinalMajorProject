using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider HealthBar;
    public Text HealthText;
    
    private int maxHealth;
    private int Health;

    public Text RunTime;
    private float RunTimer;
    private bool isRunning;
    private int SS;
    private int MM;
    private int HH;

    public Text DarkOrbCount;

    public Text EnemyPercentTxt;
    public Slider EnemyCountSlider;

    private int NumberOfEnemies;
    private int EnemiesKilled;
    
    private bool LevelLoaded;
    private bool EnemiesCounted;
    void Start()
    {
        EnemiesKilled = 0;
        LevelLoaded = false;
        EnemiesCounted = false;

        SS = 0;
        MM = 0;
        HH = 0;

        isRunning = false; //start this after player is spawned
        ChangeOrbCount(0);
        //set health and maxhealth in inventory start
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
    }
    public void IncreaseEnemiesKilled()
    {
        EnemiesKilled++;
        GetEnemyPercent();
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

        GetEnemyPercent();
    }
    private void GetEnemyPercent()
    {
        if(NumberOfEnemies > 0)
        {
            float percent = ((float)EnemiesKilled / (float)NumberOfEnemies) * 100;
            percent = Mathf.Round(percent);

            EnemyPercentTxt.text = percent + " %";
            EnemyCountSlider.value = percent;
        }
        else
        {
            print("Error: No Enemies Found");
        }
    }
}
