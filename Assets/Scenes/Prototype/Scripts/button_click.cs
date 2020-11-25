using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_click : MonoBehaviour
{
    public GameObject A;
    private terminal_main scriptA;
    private void Start()
    {
        A = transform.parent.transform.parent.gameObject;
        scriptA = A.GetComponent<terminal_main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        GameObject progress = GameObject.Find("prog_plane");
        var Renderer = progress.GetComponent<Renderer>();

        if(scriptA.progressint == 1){
            Renderer.material.SetColor("_Color", Color.green);
        }
        else{
            Renderer.material.SetColor("_Color", Color.blue);
        }
    }
}
