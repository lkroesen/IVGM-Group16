using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    int blinkTime = 99;
    void Start()
    {
        InvokeRepeating("Blinking", 1, .5f);
    }
     
    void Blinking()
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        if (--blinkTime == 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
