using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_painting : MonoBehaviour
{
    static private bool slide;
    static float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        slide = false;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime /4;
        if (slide){
            distance = distance + delta;
            if(distance < 1)
                transform.Translate(new Vector3(delta,0, 0));
        }
    }

    private void OnMouseDown() {
        slide = true;
    }
}
