using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFridge : MonoBehaviour
{
    public GameObject goal;
    public float speed = 0.1f;
    public bool moveToGoal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        moveToGoal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveToGoal) return;

        if (transform.position.z <= goal.transform.position.z)
        {
            moveToGoal = false;
            return;
        }
        
        transform.position += (Vector3.back * (speed * Time.deltaTime));
    }
}
