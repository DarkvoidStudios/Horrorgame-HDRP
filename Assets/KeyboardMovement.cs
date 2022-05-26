using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 MovementDirection;
    public int MoveSpeed = 2;
    public float MouseSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        #region BasicMovement
        //holding leftshift, move speed will be 4.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveSpeed = 4;
            //if (Input.GetKeyDown(KeyCode.W)) MovementSpeed.z += 4;
            //if (Input.GetKeyDown(KeyCode.A)) MovementSpeed.x -= 4;
            //if (Input.GetKeyDown(KeyCode.S)) MovementSpeed.z -= 4;
            //if (Input.GetKeyDown(KeyCode.D)) MovementSpeed.x += 4;
            //if (Input.GetKeyUp(KeyCode.W)) MovementSpeed.z -= 4;
            //if (Input.GetKeyUp(KeyCode.A)) MovementSpeed.x += 4;
            //if (Input.GetKeyUp(KeyCode.S)) MovementSpeed.z += 4;
            //if (Input.GetKeyUp(KeyCode.D)) MovementSpeed.x -= 4;
        }
        else
        {
            //but normaly , the move speed is  2
            MoveSpeed = 2;
            //    if (Input.GetKeyDown(KeyCode.W)) MovementSpeed.z += 2;
            //    if (Input.GetKeyDown(KeyCode.A)) MovementSpeed.x -= 2;
            //    if (Input.GetKeyDown(KeyCode.S)) MovementSpeed.z -= 2;
            //    if (Input.GetKeyDown(KeyCode.D)) MovementSpeed.x += 2;
            //    if (Input.GetKeyUp(KeyCode.W)) MovementSpeed.z -= 2;
            //    if (Input.GetKeyUp(KeyCode.A)) MovementSpeed.x += 2;
            //    if (Input.GetKeyUp(KeyCode.S)) MovementSpeed.z += 2;
            //    if (Input.GetKeyUp(KeyCode.D)) MovementSpeed.x -= 2;
        }
        //Axis Horizontal/Vertical are mapped to the arrow keys and wasd.
        // this returns 0 when not pressed.
        // -1 when arrow left, and +1 when arrow right.
        MovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // and you can multiply a Vector3 by a number.
        controller.SimpleMove(MovementDirection * MoveSpeed);
        #endregion
        #region Rotation
        //this means that this can be shortened too.
        //instead of this:
        //if (Input.GetAxis("Mouse X") > 0) transform.Rotate((Vector3.up) * MouseSpeed);
        //if (Input.GetAxis("Mouse X") < 0) transform.Rotate((Vector3.up) * -MouseSpeed);
        //use 
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * MouseSpeed);
        #endregion
    }
}