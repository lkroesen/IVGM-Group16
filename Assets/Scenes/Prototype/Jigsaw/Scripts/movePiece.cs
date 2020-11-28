using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePiece : MonoBehaviour
{

    public bool activePiece = false;

    public Rigidbody rigidboy;

    public Vector3 init_pos;

    private Vector3 collider_pos;

    private bool inCollider = false;

    public grid grid_;

    public int right_pos;

    private int seat_pos;

    // Start is called before the first frame update
    void Start()
    {
      //print(grid_.positioned[2]);
      //grid_.positioned[2]=true;
      //rigidboy = (Rigidbody) GetComponent(typeof(Rigidbody));
      // So that forces do not change our rigidboy
      //rigidboy.constraints = RigidbodyConstraints.FreezeAll;
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
        // Store Position so that when the car collides it returns to its inital position.
        init_pos = transform.position;
        activePiece = true;
    }

    private void OnMouseUp()
    {
        activePiece = false;
        if(inCollider == true){
          transform.position = collider_pos;
          inCollider = false;

          if(seat_pos==right_pos){
            grid_.positioned[right_pos-1]=true;
            print(seat_pos);
          }
          else{
            grid_.positioned[right_pos-1]=false;
          }
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
        position = new Vector3(position.x - 5*diff_x*Time.deltaTime, position.y, position.z - 5*diff_y * Time.deltaTime);
        transform.position = position;
        lastMousePoint_y = Input.mousePosition.y;
        lastMousePoint_x = Input.mousePosition.x;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "PIECE")
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = true;
        collider_pos = other.transform.position;
        seat_pos = other.GetComponent<seat>().pos;
      }
    }

    private void OnTriggerStay(Collider other)
    {
      if (other.tag == "PIECE")
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = true;
        collider_pos = other.transform.position;
        seat_pos = other.GetComponent<seat>().pos;
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "PIECE")
      {
          Physics.IgnoreCollision(other, GetComponent<Collider>());
      }
      else{
        inCollider = false;
        collider_pos = other.transform.position;
        grid_.positioned[right_pos-1]=false;
      }
    }
}
