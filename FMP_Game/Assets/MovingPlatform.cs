using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform node1;
    public Transform node2;
    private float speed;
    private Transform target;
    void Start()
    {
        target = node2;
        speed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == node1.gameObject)
        {
            target = node2;
        }
        if (collision.gameObject == node2.gameObject)
        {
            target = node1;
        }
    }
}
