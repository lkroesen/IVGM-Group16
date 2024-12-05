using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public enum Waypoints
{
    Normal, JigSaw, BPuzzle, BPuzzlePre, Safe, Fusebox, Matches
}

public class Waypoint : MonoBehaviour
{
    private GameObject _cam;
    private WaypointManager _wpm;
    private CameraMouseMovement _cmm;
    private PuzzleManager _pm;
    private GameObject tutorial_text;
    private UI_Text_Handler _uth;

    public Waypoints waypoint;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to our Camera
        _cam = GameObject.FindGameObjectWithTag("MainCamera");
        var controller = GameObject.FindGameObjectWithTag("Player");
        _wpm = controller.GetComponent<WaypointManager>();
        _pm = controller.GetComponent<PuzzleManager>();
        _uth = controller.GetComponent<UI_Text_Handler>();
        _cmm = _cam.GetComponent<CameraMouseMovement>();
        tutorial_text = GameObject.FindGameObjectWithTag("TUTORIAL");
    }

    private void puzzlePre()
    {
        _cmm.Disable_Camera_Movement = true;
        _pm.hideExterior();
    }

    private void jigsaw()
    {
        if (_uth.jigsawVisits == 0)
            _uth.JigsawText();

        _uth.jigsawVisits++;
        
        puzzlePre();
    }

    private void bPuzzle()
    {
        _uth.BPuzzleEnter();

        var l = GameObject.FindGameObjectWithTag("bp_light").GetComponent<Light>();
        l.enabled = true;
        
        puzzlePre();
        _pm.bPuzzleActive = true;
    }
    
    private void bpuzzlePrep()
    {
        
    }

    private void safe()
    {
        
        bool isPainting = GameObject.Find("Painting").GetComponent<move_painting>().isdone();
        print(isPainting);
        if(!isPainting){
            _uth.PaintingText();
        }
        else if (_uth.safeVisits == 0)
        {
            _uth.SafeText();
        }
        
        _uth.safeVisits++;
    }

    private void Fusebox(){
        int state = GameObject.Find("Painting").GetComponent<switch_script>().getState();
        if(state == 0){
            
        }
    }

    private void Matches()
    {
        puzzlePre();
        _uth.MatchPuzzleEnter();
    }

    private void OnMouseUpAsButton()
    {
        if (tutorial_text == null) return;
        if (tutorial_text.activeSelf) return;

        switch (waypoint)
        {
            case Waypoints.JigSaw : jigsaw(); break;
            case Waypoints.BPuzzle : bPuzzle(); break;
            case Waypoints.BPuzzlePre : bpuzzlePrep(); break;
            case Waypoints.Safe : safe(); break;
            case Waypoints.Fusebox : Fusebox(); break;
            case Waypoints.Matches : Matches(); break;
        }

        var parent = this.transform.parent;
        parent.gameObject.SetActive(false);

        // Move the camera to our transform
        var _p_transform = parent.transform;
        _cam.transform.position = _p_transform.position;
        _cam.transform.rotation = _p_transform.rotation;
        
        _wpm.activeLastWaypoint();
        _wpm.newWaypoint(parent.gameObject);
        
    }
}
