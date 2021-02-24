using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    int DarkOrbs;

    void Start()
    {
        DarkOrbs = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeDarkOrbs(int amount)
    {
        DarkOrbs += amount;
        print("Dark Orbs: " + DarkOrbs);
    }
}
