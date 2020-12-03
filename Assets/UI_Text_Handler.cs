﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Handler : MonoBehaviour
{
    public int jigsawVisits = 0;
    public int safeVisits = 0;
    public int bpuzzleVisists = 0;
    
    public float currentTimeout = 0;
    public GameObject text;
    private Text _text;

    private RemoteController _rc;

    public Queue<KeyValuePair<String, float>> messageQueue = new Queue<KeyValuePair<string, float>>();
    
    void Start()
    {
        _text = text.GetComponent<Text>();
        _rc = GameObject.FindGameObjectWithTag("Player").GetComponent<RemoteController>();
        enqueueText("I'd really like to watch some TV, now where did I leave that remote, it should be around here somewhere...", 9f);
    }


    private void enqueueText(string _t, float duration)
    {
        messageQueue.Enqueue(new KeyValuePair<string, float>(_t, duration));
    }
    
    /**
     * Displays the text for a certain duration.
     * _t = the text to show
     * duration = 1 = 1 second
     */
    private void showTextFor(string _t, float duration)
    {
        _text.text = _t;
        currentTimeout = duration;
        text.SetActive(true);
    }

    private void Update()
    {
        if (messageQueue.Count > 0)
        {
            var msg = messageQueue.Dequeue();
            showTextFor(msg.Key, msg.Value);
        }
        
        if (currentTimeout <= 0)
        {
            text.gameObject.SetActive(false);
        }
        currentTimeout -= Time.deltaTime;
    }

    public void JigsawText()
    {
        enqueueText("Hmm, looks like a jigsaw puzzle, wonder what shows up when I solve it", 5);
    }

    public void SafeText()
    {
        enqueueText("A safe, I can't seem to recall what I put in there, I'll need a 4 digit code to open it though", 6);
    }

    public void RemoteGet()
    {
        enqueueText("Sweet! Now I can watch some TV                                                 [Press RMB to bring up the remote]", 4);
    }


    public bool b00 = false;
    public void Batteries0()
    {
        if (!b00)
        {
            enqueueText(
                "Ofcourse... Batteries are dead, I'll have to find some new ones, I believe I keep them in a kitchen drawer",
                4);
            b00 = true;
        }
        else
            enqueueText("I should go to the kitchen, I believe the batteries are in a wooden drawer", 4);
    }

    public void Batteries1()
    {
        enqueueText("I need one more battery, I believe the same drawer also had that one.", 4);
    }

    public void BatteriesGet1()
    {
        enqueueText("One down, let's get the other one", 2.5f);
    }

    public void BatteriesGet2()
    {
        enqueueText("That's two! Now the remote should work", 2.5f);
    }
    
    
}
