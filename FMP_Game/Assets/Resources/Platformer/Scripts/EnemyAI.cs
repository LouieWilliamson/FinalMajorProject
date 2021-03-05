using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum EnemyType { sword, archer, ghost, mage, shield };
public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { patrolling, chasing, attacking, dead };

    public EnemyType type;
    private EnemyState state;

    bool levelLoaded;

    //references
    private EnemyMovement eMovement;
    private EnemyHealth eHealth;
    private EnemyAttacks eAttacks;
    private EnemyAnimations anim;
    public Transform walkCheck;
    private GameObject player;
    bool playerSet;
    public LayerMask playerLayer;

    //Chasing/Attacking
    private bool inAttackRange;
    private bool canSeePlayer;
    public float rayLength;
    public float seeDistance;
    public float attackDistance;

    //Movements
    void Start()
    {
        levelLoaded = false;
        playerSet = false;
        canSeePlayer = false;
        inAttackRange = false;
        eMovement = GetComponent<EnemyMovement>();
        anim = GetComponent<EnemyAnimations>();
        state = EnemyState.patrolling;
    }

    void Update()
    {
        //if player reference has been set then check if in view or attack range
        if(playerSet)
        {
            CheckPlayerRange();
        }

        if (inAttackRange)
        {
            state = EnemyState.attacking;
            eMovement.SetWalking(false);
        }
        else if (canSeePlayer)
        {
            state = EnemyState.chasing;
            eMovement.SetWalking(false);
        }
        else
        {
            state = EnemyState.patrolling;
            eMovement.SetWalking(true);
        }

        if (levelLoaded)
        {
            switch (state)
            {
                case EnemyState.patrolling:
                    PatrollingMovement();
                    break;
                case EnemyState.chasing:
                    ChasingMovement();
                    break;
                case EnemyState.attacking:
                    Attack();
                    break;
                default:
                    break;
            }
        }
    }
    private void CheckPlayerRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, rayLength, playerLayer);

        if (hit.collider != null)
        {
            if (hit.distance < attackDistance)
            {
                state = EnemyState.attacking;
                print("ATTACK");
            }
            else if (hit.distance < seeDistance && CheckFacingPlayer())
            {
                state = EnemyState.chasing;
                print("CHASE");
            }
        }
    }
    private bool CheckFacingPlayer()
    {
        bool facingLeft = anim.GetFacingLeft();

        float playerDirectionX = (player.transform.position - transform.position).x;
        bool playerToTheLeft = playerDirectionX <= 0;

        if(facingLeft == playerToTheLeft)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void PatrollingMovement()
    {
        if(type == EnemyType.ghost)
        {
            //fly around and teleport
        }
        else
        {
            //walk until there's a gap or wall then turn around
        }
    }
    private void Attack()
    {
        //if within attacking range, choose a random attack
    }
    private void ChasingMovement()
    {
        if (type == EnemyType.ghost)
        {
            //fly towards the player
        }
        else
        {
            //pathfind and move towards player
        }
    }
    public void SetPlayer(GameObject p)
    {
        player = p;
        playerSet = true;
    }
    public void SetDead()
    {
        state = EnemyState.dead;
        Destroy(this);
    }
}
