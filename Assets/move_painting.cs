using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_painting : MonoBehaviour
{
    static private bool slide;
    static private bool done;
    static float distance = 0;
    private UI_Text_Handler _uth;
    // Start is called before the first frame update
    void Start()
    {
        slide = false;
        done = false;
        var controller = GameObject.FindGameObjectWithTag("Player");
        _uth = controller.GetComponent<UI_Text_Handler>();
    }

    public bool isdone(){
        return done;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        if (slide){
            distance = distance + delta;
            if(distance < 1){
                transform.Translate(new Vector3(delta,0, 0));
            }
            else{
                done = true;
                _uth.SafeText();
            }
        }
    }

    private void OnMouseDown() {
        slide = true;
    }
}
