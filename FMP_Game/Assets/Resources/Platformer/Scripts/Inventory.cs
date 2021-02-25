using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    int DarkOrbs;
    public int baseHealth;
    public int maxHealth;
    public int healthMultiplier;
    int health;
    private HitEffect hitEffect;
    void Start()
    {
        hitEffect = GetComponent<HitEffect>();
        health = baseHealth * healthMultiplier;
        maxHealth = health;
        DarkOrbs = 0;
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
        }
        health += amount;
        print("Hit");
    }
    public void ChangeDarkOrbs(int amount)
    {
        DarkOrbs += amount;
    }
    private void Kill()
    {
        print("You dead bro");
    }
}
