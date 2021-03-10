using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bg;
    public GameObject fg;
    private Rigidbody2D fg_rb;
    private GameObject player;
    private Rigidbody2D p_rb;
    public float speed;

    public Transform leftBound;
    public Transform rightBound;

    private bool levelLoaded;
    private bool playerSaved;
    public Camera cam;
    void Start()
    {
        fg_rb = fg.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
            fg_rb.velocity = new Vector2(-cam.velocity.x * speed, -cam.velocity.y * speed);
    }
}
