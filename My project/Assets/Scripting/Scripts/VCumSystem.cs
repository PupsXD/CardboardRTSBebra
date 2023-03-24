using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCumSystem : MonoBehaviour
{
    // Speed at which the player moves
    public float moveSpeed = 50f;

    // Speed at which the player rotates
    public float rotateSpeed = 300f;

    // Size of the edge scrolling area
    public int edgeScrollSize = 20;

    //Drag mouse
    private bool dragPanMove;
    private Vector2 lastMousePosition;
    public float dragPanSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        // Determine the input direction from WASD keys and edge scrolling
        Vector3 inputDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height - edgeScrollSize)
        {
            inputDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < edgeScrollSize)
        {
            inputDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x < edgeScrollSize)
        {
            inputDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - edgeScrollSize)
        {
            inputDir.x = +1f;
        }
        
        //Drag movement 

        #region DragMovement

        if (Input.GetMouseButtonDown(1))
        {
            dragPanMove = true;
            lastMousePosition = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            dragPanMove = false;
        }

        if (dragPanMove)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.z = mouseMovementDelta.y * dragPanSpeed;

        }

            #endregion

        // Determine the move direction from the input direction
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        // Normalize the move direction to ensure consistent movement speed
        moveDir.Normalize();

        // Move the player based on the move direction and speed
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Determine the rotation direction from Q and E keys
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        // Rotate the player based on the rotation direction and speed
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }
}
