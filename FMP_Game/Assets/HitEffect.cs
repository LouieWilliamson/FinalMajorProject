using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    bool beenHit;
    float effectTimer;
    float flashTime;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        beenHit = false;
        effectTimer = 0;
        flashTime = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (beenHit)
        {
            sr.color = Color.red;
            effectTimer += Time.deltaTime;

            if (effectTimer > flashTime)
            {
                beenHit = false;
                sr.color = Color.white;
            }
        }
    }

    public void Enable()
    {
        beenHit = true;
    }
}
