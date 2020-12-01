using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPick : MonoBehaviour
{
    public GameObject Pick;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam  = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        Vector3 point = new Vector3();
        Event   currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = Input.mousePosition.x;
        mousePos.y = cam.pixelHeight - Input.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        print(point.x);
    }
}
