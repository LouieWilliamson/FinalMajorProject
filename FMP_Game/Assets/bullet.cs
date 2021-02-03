using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    bool isRight;
    Rigidbody2D m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        isRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            m_rb.velocity = new Vector2(1, 0);
        }
        else
        {
            m_rb.velocity = new Vector2(-1, 0);
        }
    }

    public void SetDirection(bool RightIfTrue)
    {
        isRight = RightIfTrue;
    }
}
