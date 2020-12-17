using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Handler : MonoBehaviour
{
    public int jigsawVisits = 0;
    public int safeVisits = 0;
    public bool solvedBPuzzle = false;
    public bool solvedMatchPuzzle = false;
    
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

    public void PaintingText(){
        enqueueText("This painting is so .......... inspiring .......", 5);
    }

    public void SafeText()
    {
        enqueueText("A safe! I can't remember what I put in there, but it seems to need a code...", 6);
    }

    public void FuseboxText(int state){
        if(state == 0){
            enqueueText("The power seems fine for now.", 4);
        }
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

    public void BPuzzleEnter()
    {
        if (solvedBPuzzle) return;
        
        enqueueText(
            solvedMatchPuzzle
                ? "Now that I have a magnet I should be able to move the objects behind the glass!"
                : "Hmm, no way for me to move these batteries, I'd need something to move it through the glass", 4);
    }

    public void MatchPuzzleEnter()
    {
        if (solvedMatchPuzzle) return;
        
        enqueueText("Hmm, so what have we got here, looks like the order of these matches is important...", 4);
    }
    
    public void preWinMatchPuzzle()
    {
        if (solvedMatchPuzzle) return;

        enqueueText("Oh part of the match came off?", 3);
    }

    public void winMatchPuzzle()
    {
        if (solvedMatchPuzzle) return;
        
        solvedMatchPuzzle = true;
        enqueueText("Looks like a magnet, where do I need to use this?", 4);
    }
    
}
