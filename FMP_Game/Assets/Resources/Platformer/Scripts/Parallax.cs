using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Rigidbody2D fg_rb;
    public float speed;
    public Camera cam;

    private void FixedUpdate()
    {
        fg_rb.velocity = -cam.velocity * speed;
    }
}
