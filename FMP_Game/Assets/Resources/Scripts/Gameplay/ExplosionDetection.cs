using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionRadius;
    public LayerMask enemyLayer;

    private void Start()
    {
        //get all enemy objects in the blast radius
        Collider2D[] explosionDetections = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);

        //for each enemy, apply damage
        for (int i = 0; i < explosionDetections.Length; i++)
        {
            if (explosionDetections[i] != null)
            {
                if (explosionDetections[i].gameObject.tag == "Enemy")
                {
                    EnemyHealth targetHealth = explosionDetections[i].gameObject.GetComponent<EnemyHealth>();
                    targetHealth.ApplyDamage(targetHealth.health);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
