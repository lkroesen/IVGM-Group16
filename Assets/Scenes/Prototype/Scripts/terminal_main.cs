using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminal_main : MonoBehaviour
{
    public int progressint;
    // Start is called before the first frame update
    void Start()
    {
        progressint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(progressint);
    }
}
