using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType { sword, archer, ghost, mage, shield };
public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { patrolling, chasing, attacking, dead };

    public EnemyType type;
    private EnemyState state;

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
    private float attackTimer;
    private float attackTime;
    public int HeavyAttackChance;

    //Movements
    private bool walkingRight;
    public LayerMask groundLayer;

    void Start()
    {
        attackTime = 1;
        attackTimer = 0;
        walkingRight = true;
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

        if(state != EnemyState.attacking && attackTimer > 0)
        {
            attackTimer = 0;
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

        if (playerSet)
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
    //Checks distance to player and whether the enemy is facing the player to set state to attacking or chasing.
    private void CheckPlayerRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, rayLength, playerLayer);

        if (hit.collider != null)
        {
            if (hit.distance < attackDistance)
            {
                inAttackRange = true;
                print("ATTACK");
            }
            else if (hit.distance < seeDistance && CheckFacingPlayer())
            {
                canSeePlayer = true;
                inAttackRange = false;
                print("CHASE");
            }
            else if (hit.distance < seeDistance && canSeePlayer)
            {
                canSeePlayer = true;
                inAttackRange = false;
            }
            else
            {
                inAttackRange = false;
                canSeePlayer = false;
            }
        }
    }
    //Check whether the enemy is facing the player or not
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
            PatrollingCheck();
            eMovement.Move(walkingRight);
        }
    }
    private void PatrollingCheck()
    {
        Vector2 wallVect;

        if(walkingRight)
        {
            wallVect = Vector2.right;
        }
        else
        {
            wallVect = Vector2.left;
        }

        RaycastHit2D floorRay = Physics2D.Raycast(walkCheck.position, Vector2.down, 1, groundLayer);
        RaycastHit2D wallRay = Physics2D.Raycast(walkCheck.position, wallVect, 0.1f, groundLayer);


        if (floorRay.collider == null || wallRay.collider != null)
        {
            walkingRight = !walkingRight;
        }
    }
    private void Attack()
    {
        //if within attacking range, choose a random attack
        //anim.Idle();
        eMovement.StopHorizontal();

        if(!CheckFacingPlayer())
        {
            anim.FlipEnemy();
        }

        attackTimer += Time.deltaTime;

        if (attackTimer > attackTime)
        {
            int attackType = Random.Range(1, 101);

            if(attackType <= HeavyAttackChance)
            {
                anim.HeavyAttack();
            }
            else
            {
                anim.LightAttack();
            }

            attackTimer = 0;
        }
    }
    private void ChasingMovement()
    {
        if (!CheckFacingPlayer())
        {
            anim.FlipEnemy();
        }
        
        if (type == EnemyType.ghost)
        {
            //fly towards the player
        }
        else
        {
            bool playerOnTheLeft = (player.transform.position - transform.position).x <= 0;

            if (playerOnTheLeft)
            {
                walkingRight = false;
            }
            else
            {
                walkingRight = true;
            }

            if (ChasingCheck())
            {
                eMovement.Move(walkingRight);
            }
            else
            {
                eMovement.StopHorizontal();
            }
        }
    }
    private bool ChasingCheck()
    {
        Vector2 wallVect;

        if (walkingRight)
        {
            wallVect = Vector2.right;
        }
        else
        {
            wallVect = Vector2.left;
        }

        RaycastHit2D floorRay = Physics2D.Raycast(walkCheck.position, Vector2.down, 1, groundLayer);
        RaycastHit2D wallRay = Physics2D.Raycast(walkCheck.position, wallVect, 0.1f, groundLayer);


        if (floorRay.collider == null || wallRay.collider != null)
        {
            return false;
        }
        else
        {
            return true;
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
