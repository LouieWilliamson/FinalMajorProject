using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    EnemyAnimations anim;
    private EnemyType type;
    public int playerKnockBack;

    public int LightDamage;
    public Transform lightAttackPointA;
    public Transform lightAttackPointB;
    //public float lightRange;

    public int HeavyDamage;
    public Transform heavyAttackPointA;
    public Transform heavyAttackPointB;
    //public float heavyRange;

    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<EnemyAnimations>();
        type = GetComponent<EnemyAI>().type;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightAttack();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            HeavyAttack();
        }
    }
    public void LightAttack()
    {
        Collider2D hit = Physics2D.OverlapArea(lightAttackPointA.position, lightAttackPointB.position, playerLayer);

        if (hit != null)
        {
            hit.GetComponent<Inventory>().ChangeHealth(-LightDamage);

            if (PlayerToTheRight(hit.gameObject))
            {
                hit.GetComponent<PlayerMovement>().Knockback(playerKnockBack, false);
            }
            else
            {
                hit.GetComponent<PlayerMovement>().Knockback(-playerKnockBack, false);
            }
        }
        else
        {
            print("Light Missed");
        }
    }
    public void HeavyAttack()
    {
        Collider2D hit = Physics2D.OverlapArea(lightAttackPointA.position, lightAttackPointB.position, playerLayer);

        if (hit != null)
        {
            hit.GetComponent<Inventory>().ChangeHealth(-HeavyDamage);

            if (PlayerToTheRight(hit.gameObject))
            {
                hit.GetComponent<PlayerMovement>().Knockback(playerKnockBack * 2, false);
            }
            else
            {
                hit.GetComponent<PlayerMovement>().Knockback(-playerKnockBack * 2, false);
            }
        }
        else
        {
            print("Heavy Missed");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(lightAttackPointA.position, lightAttackPointB.position);

        Gizmos.color = Color.white;

        Gizmos.DrawLine(heavyAttackPointA.position, heavyAttackPointB.position);

    }
    private bool PlayerToTheRight(GameObject p)
    {
        float playerX = (p.transform.position - transform.position).x;
        bool playerToTheLeft = playerX <= 0;

        if (playerToTheLeft)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
