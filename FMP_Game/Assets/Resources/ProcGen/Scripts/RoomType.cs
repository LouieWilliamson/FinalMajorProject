using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    // 0 -- LR
    // 1 -- LRB
    // 2 -- LRT
    // 3 -- LRTB
    public void DestroyRoom()
    {
        Destroy(gameObject);
    }
}
