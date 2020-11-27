using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealRemoteBaseScript : MonoBehaviour
{
    public GameObject tv;
    
    public void registerButtonClick(string value)
    {
        switch (value)
        {
            case "E" : 
                returnToStart();
                break;
            case "P" : 
                powerButton();
                break;
            case "L" : return;
            case "H" :
                hButton();
                break;
            case "V" : return;
            case "C" : return;
            // Numbers
            case "1" : return;
            case "2" : return;
            case "3" : return;
            case "4" : return;
            case "5" : return;
            case "6" : return;
            case "7" : return;
            case "8" : return;
            case "9" : return;
            case "0" : return;
        }
    }

    void hButton()
    {
        
    }

    void returnToStart()
    {
        var _cam = GameObject.FindGameObjectWithTag("MainCamera");
        // TODO: provide prefab of the original waypoint.
    }

    public bool isTvOn = false;
    private static readonly int COLOR = Shader.PropertyToID("_Color");

    void powerButton()
    {
        var meshRenderer = tv.GetComponent<MeshRenderer>();
        var material = meshRenderer.material;

        if (!isTvOn)
        {
            material.SetColor(COLOR, Color.white);
            isTvOn = true;
        }
        else
        {
            material.SetColor(COLOR, Color.black);
            isTvOn = false;
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
