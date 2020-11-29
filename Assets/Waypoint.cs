using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private GameObject _cam;
    private WaypointManager _wpm;
    private CameraMouseMovement _cmm;
    private PuzzleManager _pm;
    private GameObject tutorial_text;
    
    public bool isJigSawWaypoint = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to our Camera
        _cam = GameObject.FindGameObjectWithTag("MainCamera");
        var controller = GameObject.FindGameObjectWithTag("Player");
        _wpm = controller.GetComponent<WaypointManager>();
        _pm = controller.GetComponent<PuzzleManager>();
        _cmm = _cam.GetComponent<CameraMouseMovement>();
        tutorial_text = GameObject.FindGameObjectWithTag("TUTORIAL");
        
    }

    private void OnMouseUpAsButton()
    {
        if (tutorial_text == null) return;
        if (tutorial_text.activeSelf) return;
        
        
        if (isJigSawWaypoint)
        {
            _cmm.Disable_Camera_Movement = true;
            _pm.hideExterior();
        }
        
        var parent = this.transform.parent;
        parent.gameObject.SetActive(false);

        // Move the camera to our transform
        var _p_transform = parent.transform;
        _cam.transform.position = _p_transform.position;
        _cam.transform.rotation = _p_transform.rotation;
        
        _wpm.activeLastWaypoint();
        _wpm.newWaypoint(parent.gameObject);
        
    }
}
