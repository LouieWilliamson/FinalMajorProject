using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    EnemyAnimations anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<EnemyAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            LightAttack();
        }
        if (Input.GetKey(KeyCode.G))
        {
            HeavyAttack();
        }
    }

    private void LightAttack()
    {
        anim.LightAttack();
    }
    private void HeavyAttack()
    {
        anim.HeavyAttack();
    }
}
