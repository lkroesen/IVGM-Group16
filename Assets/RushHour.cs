using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHour : MonoBehaviour
{
    public bool isActive = true;
    private CameraMouseMovement _cmm;

    // Start is called before the first frame update
    void Start()
    {
        // Get Our Camera
        _cmm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMouseMovement>();
        _cmm.Disable_Camera_Movement = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
