using UnityEngine;

public class RealRemoteBaseScript : MonoBehaviour
{
    public GameObject tv;
    public GameObject over_tv;
    private UI_Text_Handler _uth;
    private RemoteController _rc;
    private switch_script _ss;
    private CypherHandler _ch;
    
    private void Start()
    {
        var controller = GameObject.FindGameObjectWithTag("Player");
        _rc = controller.GetComponent<RemoteController>();
        _uth = controller.GetComponent<UI_Text_Handler>();
        _ss = GameObject.FindGameObjectWithTag("fusebox").GetComponent<switch_script>();
        _ch = GameObject.FindGameObjectWithTag("cypher").GetComponent<CypherHandler>();
    }

    public void registerButtonClick(string value)
    {
        switch (value)
        {
            case "E" :
                Return();
                break;
            case "L" : return;
            case "H" :
                hButton();
                break;
            case "V" : return;
            case "C" : return;
            default:
            {
                if (!Batteries()) return;
        
                switch (value)
                {
                    case "P" : 
                        powerButton();
                        break;
                    case "1" : break;
                    case "2" : break;
                    case "3" : break;
                    case "4" : break;
                    case "5" : break;
                    case "6" : break;
                    case "7" : break;
                    case "8" : break;
                    case "9" : break;
                    case "0" : break;
                }

                break;
            }
        }
    }

    bool Batteries()
    {
        switch (_rc.workingBatteries)
        {
            case 0:
                _uth.Batteries0();
                return false;
            case 1:
                _uth.Batteries1();
                return false;
            default:
                return true;
        }
    }
    
    void hButton()
    {
        
    }

    public static void Return()
    {
        var _gameObject = GameObject.FindGameObjectWithTag("Player");
        var _cam = GameObject.FindGameObjectWithTag("MainCamera");
        var _wpm = _gameObject.GetComponent<WaypointManager>();
        var _cmm = _cam.GetComponent<CameraMouseMovement>();
        
        if (_cmm.Disable_Camera_Movement)
            PuzzleExit(_cmm, _gameObject);
        
        var wp = _wpm.returnFunc();
        if (wp == null) 
            return;
        else
        {
            _wpm.newWaypoint(wp);
            _cam.transform.position = wp.transform.position;
            _cam.transform.rotation = wp.transform.rotation;
        }
    }

    private static void PuzzleExit(CameraMouseMovement _cmm, GameObject gamecontroller)
    {
        _cmm.Disable_Camera_Movement = false;
        var _pm = gamecontroller.GetComponent<PuzzleManager>();

        if (_pm.bPuzzleActive)
        {
            _pm.bPuzzleActive = false;
            GameObject.FindGameObjectWithTag("bp_light").GetComponent<Light>().enabled = false;
            _pm.syncBPuzzle();
        }

        _pm.showExterior();

    }

    public bool isTvOn = false;
    private static readonly int COLOR = Shader.PropertyToID("_Color");

    public GameObject lights;
    public bool triggerBlackout = true;
        
    void powerButton()
    {
        print("Pressing powerbutton.");
        
        isTvOn = !isTvOn;

        if (_uth.blackout)
        {
            lights.GetComponent<lighting>().setAllLighting(0.1f);
            _ss.activate();
            _ch.showText();
            _uth.Blackout();
        }
        else
        {
            over_tv.SetActive(isTvOn);
        }
        
        

        
        /*
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
        */
    }
}
