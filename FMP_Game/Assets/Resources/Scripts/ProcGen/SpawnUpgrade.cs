using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUpgrade : MonoBehaviour
{
    public GameObject Upgrade;
    private Upgrade type;
    public int spawnPercent;
    // Start is called before the first frame update
    void Start()
    {
        int chancetospawn = Random.Range(1, 101);

        if (chancetospawn <= spawnPercent)
        {
            GameObject upgrade = Instantiate(Upgrade, transform.position, transform.rotation);

            int randomType = Random.Range(1,4);

            switch (randomType)
            {
                case 1:
                    type = global::Upgrade.Grenade;
                    break;
                case 2:
                    type = global::Upgrade.Laser;
                    break;
                case 3:
                    type = global::Upgrade.Cooldown;
                    break;
                default:
                    type = global::Upgrade.Cooldown;
                    break;
            }

            upgrade.GetComponent<WeaponUpgrade>().type = type;
        }
    }
}
