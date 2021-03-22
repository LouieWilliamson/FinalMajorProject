using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFace : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator FaceAnim;
    bool faceActive;
    bool playerTeleported;
    private void Start()
    {
        faceActive = false;
        playerTeleported = false;

    }
    void Update()
    {
        if(faceActive && !playerTeleported)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                print("TELEPORT");
                playerTeleported = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FaceAnim.SetTrigger("In");
            faceActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FaceAnim.SetTrigger("Out");
            faceActive = false;
        }
    }
}
