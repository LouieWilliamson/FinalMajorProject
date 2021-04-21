using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDelete : MonoBehaviour
{
    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
