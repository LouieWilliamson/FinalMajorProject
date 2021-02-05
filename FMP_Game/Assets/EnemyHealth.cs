using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    private HitEffect hitEffect;
    void Start()
    {
        health = 100;
        hitEffect = GetComponent<HitEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ApplyDamage(int damage)
    {
        health += -damage;
        hitEffect.Enable();
    }
}
