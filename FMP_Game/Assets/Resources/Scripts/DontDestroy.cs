using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("Manager");
        
        if (managers.Length > 1) Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }
}
