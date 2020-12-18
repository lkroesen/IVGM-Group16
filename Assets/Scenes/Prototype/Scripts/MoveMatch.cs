using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMatch : MonoBehaviour
{
    public bool activePiece = false;

    public Rigidbody _rigidbody;

    public Vector3 init_pos;

    private Vector3 collider_pos;

    private bool inCollider = false;

    private MagnetSpawner _ms;
    
    // Start is called before the first frame update
    private void Start()
    {
        _ms = this.transform.parent.gameObject.GetComponent<MagnetSpawner>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        // Store Position so that match can return to initial position
        init_pos = transform.position;
    }

    // Update is called once per frame
    private float? lastMousePoint_x = null;
    private float? lastMousePoint_y = null;
    void Update()
    {
        if (!activePiece) return;
        movePieceXY();
    }

    private void OnMouseDown()
    {        
        activePiece = true;
    }

    private void OnMouseUp()
    {
        activePiece = false;
        if(inCollider == true){
          transform.position = collider_pos;
          inCollider = false;
        }
        if(this.tag != "Correct"){
          transform.position = init_pos;
        }
        else if(transform.position.x - init_pos.x < -0.15f && transform.position.x - init_pos.x > -0.25f &&
                transform.position.z - init_pos.z < 0.05f && transform.position.z - init_pos.z > -0.05f){
          var position = new Vector3(init_pos.x - 0.2f, transform.position.y, init_pos.z);
          transform.position = position;
          var controller = GameObject.FindGameObjectWithTag("Player");
          var _uth = controller.GetComponent<UI_Text_Handler>();
          _uth.preWinMatchPuzzle();
          Instantiate(_ms.magnet, _ms.magnet_wp.transform);
        }
        else{
          transform.position = init_pos;
        }
    }

    private void movePieceXY()
    {
        if (Input.GetMouseButtonDown(0))
        {
          lastMousePoint_x = Input.mousePosition.x;
          lastMousePoint_y = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0)) {
            lastMousePoint_x = null;
            lastMousePoint_y = null;
        }

        if (lastMousePoint_x == null) return;
        if (lastMousePoint_y == null) return;

        var diff_x = Input.mousePosition.x - lastMousePoint_x.Value;
        var diff_y = Input.mousePosition.y - lastMousePoint_y.Value;
        var position = transform.position;
        position = new Vector3(position.x - diff_x * Time.deltaTime * 0.27f, position.y, position.z - diff_y * Time.deltaTime * 0.27f);
        transform.position = position;
        lastMousePoint_y = Input.mousePosition.y;
        lastMousePoint_x = Input.mousePosition.x;
    }
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Match")
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = true;
        collider_pos = other.transform.position;
      }
    }

    private void OnTriggerStay(Collider other)
    {
      if (other.tag == "Match")
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = true;
        collider_pos = other.transform.position;
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Match")
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = false;
        collider_pos = other.transform.position;
      }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Match")
      {
          Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
      }
    }
}
