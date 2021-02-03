using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    PlayerAnimations p_Anim;
    void Start()
    {
        p_Anim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        //Instantiate(bullet, transform);
        print("SHOOT");
    }
}
