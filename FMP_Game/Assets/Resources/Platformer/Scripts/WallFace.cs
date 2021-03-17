using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFace : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator FaceAnim;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Move Face In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Move Face Out");
        }
    }
}
