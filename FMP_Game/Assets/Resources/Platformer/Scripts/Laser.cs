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
    }
    public void DeactivateLaser()
    {
        lineRenderer.enabled = false;
        fireLight.enabled = false;
        lineRenderer.SetPosition(1, Vector2.zero);
        laserEndPoint.localPosition = Vector2.zero;
    }
    public void UpdateLaser()
    {
        //lineRenderer.SetPosition(0, transform.position);

        Vector2 laserEnd = new Vector2(lineRenderer.GetPosition(1).x, 0);
        laserEnd.x += laserFireSpeed * Time.deltaTime;

        lineRenderer.SetPosition(1, laserEnd);

        laserEndPoint.localPosition = laserEnd;

        //Vector2 laserDirection = laserEnd
    }
}
