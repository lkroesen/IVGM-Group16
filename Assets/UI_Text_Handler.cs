using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Handler : MonoBehaviour
{
    public float currentTimeout = 0;

    public GameObject text;

    private Text _text;
    
    void Start()
    {
        _text = text.GetComponent<Text>();
        
        showTextFor("I'd really like to watch some TV, now where did I leave that remote, it should be around here somewhere...", 9f);
    }
    
    
    /**
     * Displays the text for a certain duration.
     * _t = the text to show
     * duration = 1 = 1 second
     */
    void showTextFor(string _t, float duration)
    {
        _text.text = _t;
        currentTimeout = duration;
        text.SetActive(true);
    }

    private void Update()
    {
        if (currentTimeout <= 0)
        {
            text.gameObject.SetActive(false);
        }
        else
        {
            currentTimeout -= Time.deltaTime;
        }
    }
}
