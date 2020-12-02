using System;
using UnityEngine;

/**
 * Created by Laurens K
 */
public class CameraMouseMovement : MonoBehaviour
{
    // Modify how fast the camera moves when the mouse is moved.
    public float speed = 100.0f;

    // Limit the lower angle so the camera is not going to spin out of control.
    public float Horizontal_Camera_Angle_Limit = 35f;
    
    // Useful when switching to the "Puzzle Mode" so that the user doesn't move the camera when interacting with the puzzle.
    public bool Disable_Camera_Movement = false;

    public float zoomSpeed;

    private Camera _cam;
    
    // Ensures the cursor is visible, as the camera is always present in the scene, this code will always run.
    void Start()
    {
        _cam = GetComponent<Camera>();
        Cursor.visible = true;
        
        // Maybe enable so that the mouse will stay confined to the screen (annoyance with multiple screens).
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Helper function to actually limit the camera.
    float camera_horizontal_limiter(float eulerx, float ymove, float angleLimit)
    {
        var x = eulerx + ymove;
        if (!(x > angleLimit) || !(x < 360-angleLimit)) return x;
        return x > 360-angleLimit-30 ? 360-angleLimit : angleLimit;
    }
    
    // Main Camera Movement Logic
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_cam.fieldOfView >= 60)
                return;
            _cam.fieldOfView += zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_cam.fieldOfView <= 30)
                return;
            _cam.fieldOfView -= zoomSpeed;
        }
        
        // Disables camera movment
        if (Disable_Camera_Movement) return;
        
        // Camera can only move when the left mouse button has been pressed
        if (!Input.GetMouseButton(0)) return;

        // Don't move camera when the user simply clicks and a very small amount of movement is created.
        if (Math.Abs(Input.GetAxis("Mouse X")) < 0) return;

        // Get rotational angle
        var euler = transform.eulerAngles;
        // Calculate velocity
        var velocity = Time.deltaTime * speed;
        
        // Create movement based on mouse
        var xmove = Input.GetAxisRaw("Mouse X") * -velocity;
        var ymove = Input.GetAxisRaw("Mouse Y") * velocity;

        // Calculate new angles
        var x = camera_horizontal_limiter(euler.x, ymove, Horizontal_Camera_Angle_Limit);
        var y = euler.y + xmove;
        var nextEuler = new Vector3(x, y , euler.z);
        
        // Apply to the transform
        transform.eulerAngles = nextEuler;
    }
}
