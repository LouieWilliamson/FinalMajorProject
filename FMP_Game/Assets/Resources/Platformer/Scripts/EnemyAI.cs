using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { sword, archer, ghost, mage, shield };
    public enum EnemyState { patrolling, chasing, attacking, dead };

    public EnemyType type;
    private EnemyState state;

    private EnemyMovement eMovement;
    private EnemyHealth eHealth;
    private EnemyAttacks eAttacks;

    public Transform walkCheck;
    private GameObject player;
    void Start()
    {
        eMovement = GetComponent<EnemyMovement>();
        state = EnemyState.patrolling;
    }

    void Update()
    {
        
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
        //pathfind and move towards player
    }
}
