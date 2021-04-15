using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lineRenderer;
    public Light2D fireLight;
    public float laserFireSpeed;

    public Transform laserEndPoint;
    public LayerMask enemiesLayer;
    public LayerMask environmentLayer;

    public ParticleSystem GunFX1;
    public ParticleSystem GunFX2;
                              
    public ParticleSystem HitFX1;
    public ParticleSystem HitFX2;
    public void ActivateLaser()
    {
        laserEndPoint.localPosition = Vector2.zero;
        lineRenderer.enabled = true;
        fireLight.enabled = true;

        GunFX1.Play();
        GunFX2.Play();
    }
    public void DeactivateLaser()
    {
        lineRenderer.enabled = false;
        fireLight.enabled = false;
        lineRenderer.SetPosition(1, Vector2.zero);

        GunFX1.Stop();
        GunFX2.Stop();
        HitFX1.Stop();
        HitFX2.Stop();
    }
    public void UpdateLaser()
    {

        Vector2 laserEnd = new Vector2(lineRenderer.GetPosition(1).x, 0);
        laserEnd.x += laserFireSpeed * Time.deltaTime;

        lineRenderer.SetPosition(1, laserEnd);

        laserEndPoint.localPosition = laserEnd;
        
        Vector2 laserDirection = (Vector2)laserEndPoint.position - (Vector2)transform.position;

        RaycastHit2D environmentRay = Physics2D.Raycast(transform.position, laserDirection, laserDirection.magnitude, environmentLayer);
        RaycastHit2D enemyRay = Physics2D.Raycast(transform.position, laserDirection, laserDirection.magnitude, enemiesLayer);


        if(enemyRay)
        {
            laserEndPoint.position = enemyRay.point;
            lineRenderer.SetPosition(1, laserEndPoint.localPosition);

            if (!HitFX1.isPlaying) HitFX1.Play();
            if (!HitFX2.isPlaying) HitFX2.Play();

            EnemyHealth eHealth = enemyRay.collider.gameObject.GetComponent<EnemyHealth>();
            eHealth.ApplyDamage(eHealth.health);
        }
        if (environmentRay)
        {
            laserEndPoint.position = environmentRay.point;
            lineRenderer.SetPosition(1, laserEndPoint.localPosition);

            if (!HitFX1.isPlaying) HitFX1.Play();
            if (!HitFX2.isPlaying) HitFX2.Play();
        }
    }
}
