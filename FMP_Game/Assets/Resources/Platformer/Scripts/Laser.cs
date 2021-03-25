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

    public GameObject GunFX;
    public GameObject HitFX;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActivateLaser()
    {
        lineRenderer.enabled = true;
        fireLight.enabled = true;
        GunFX.SetActive(true);
    }
    public void DeactivateLaser()
    {
        lineRenderer.enabled = false;
        fireLight.enabled = false;
        lineRenderer.SetPosition(1, Vector2.zero);
        laserEndPoint.localPosition = Vector2.zero;
        HitFX.SetActive(false);
        GunFX.SetActive(false);
    }
    public void UpdateLaser()
    {
        //lineRenderer.SetPosition(0, transform.position);

        Vector2 laserEnd = new Vector2(lineRenderer.GetPosition(1).x, 0);
        laserEnd.x += laserFireSpeed * Time.deltaTime;

        lineRenderer.SetPosition(1, laserEnd);

        laserEndPoint.localPosition = laserEnd;
        
        Vector2 laserDirection = (Vector2)laserEndPoint.position - (Vector2)transform.position;

        RaycastHit2D environmentRay = Physics2D.Raycast(transform.position, laserDirection, laserDirection.magnitude, environmentLayer);
        RaycastHit2D enemyRay = Physics2D.Raycast(transform.position, laserDirection, laserDirection.magnitude, enemiesLayer);

        //Debug.DrawLine(transform.position, laserEndPoint.position, Color.cyan);

        if(enemyRay)
        {
            laserEndPoint.position = enemyRay.point;
            lineRenderer.SetPosition(1, laserEndPoint.localPosition);
            if (!HitFX.activeInHierarchy) HitFX.SetActive(true);
        }
        if (environmentRay)
        {
            laserEndPoint.position = environmentRay.point;
            lineRenderer.SetPosition(1, laserEndPoint.localPosition);
            if (!HitFX.activeInHierarchy) HitFX.SetActive(true);
        }
    }
}
