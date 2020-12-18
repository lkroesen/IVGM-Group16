using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CypherHandler : MonoBehaviour
{
    public GameObject text;
    public GameObject glow;
    
    private UI_Text_Handler _uth;
    private switch_script _ss;
    
    // Start is called before the first frame update
    void Start()
    {
        var controller = GameObject.FindGameObjectWithTag("Player");
        _uth = controller.GetComponent<UI_Text_Handler>();
        _ss = GameObject.FindGameObjectWithTag("fusebox").GetComponent<switch_script>();
        
        hideText();
    }

    public void hideText()
    {
        text.gameObject.SetActive(false);
        glow.gameObject.SetActive(false);
    }

    public void showText()
    {
        text.gameObject.SetActive(true);
        glow.gameObject.SetActive(true);
    }
    
}
