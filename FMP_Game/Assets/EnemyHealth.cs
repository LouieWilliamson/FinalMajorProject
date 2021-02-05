using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    private HitEffect hitEffect;
    public GameObject deathFXPrefab;
    private GameObject deathFX;
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
            deathFX = Instantiate(deathFXPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void ApplyDamage(int damage)
    {
        health += -damage;
        hitEffect.Enable();
    }
}
