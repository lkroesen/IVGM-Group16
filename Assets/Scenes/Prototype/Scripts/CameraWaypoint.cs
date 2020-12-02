using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypoint : MonoBehaviour
{
    private GameObject _cam;
    public GameObject _cam_waypoint_token;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to our Camera
        _cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // When it gets clicked
    private void OnMouseDown()
    {
        var _cam_transform = _cam.transform;
        
        // Leave a cube to go back to our orignal waypoint
        Instantiate(_cam_waypoint_token, _cam_transform.position, _cam_transform.rotation);

        // Move the camera to our transform
        _cam.transform.position = transform.position;
        _cam.transform.rotation = transform.rotation;
        
        // Deactivate our game object
        Destroy(transform.gameObject);
        
        // TODO: Do not destroy object, deactivate instead, need list of all waypoints so that return works properly.
    }
}
