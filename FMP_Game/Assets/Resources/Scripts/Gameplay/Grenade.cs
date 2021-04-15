using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canFire;
    private float shootTimer;
    private float timeToShoot;

    private bool active;
    
    public GameObject shell;
    private GameObject grenadeInstance;
    public Transform EndOfGun;
    //spawn grenade, add force
    public Vector2 grenadeForce;
    void Start()
    {
        canFire = true;
        shootTimer = 0;
        timeToShoot = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= timeToShoot)
            {
                canFire = true;
                shootTimer = 0;
            }
        }
    }
    public void Fire(bool facingLeft)
    {
        if (canFire)
        {
            grenadeInstance = Instantiate(shell, EndOfGun.position, EndOfGun.rotation);

            if (facingLeft)
            {
                grenadeInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(-grenadeForce.x, grenadeForce.y));
            }
            else
            {
                grenadeInstance.GetComponent<Rigidbody2D>().AddForce(grenadeForce);
            }

            canFire = false;
        }
    }
}
