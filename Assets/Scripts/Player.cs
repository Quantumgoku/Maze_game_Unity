using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public float wSpeed = 2.5f;
    public float rSpeed = 1.5f;
    public float jump = 2.7f;
    public float gravity = 9.8f;
    public Camera playerCamera;
    public float lSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? rSpeed : wSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? rSpeed : wSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jump;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            rotationX += Input.GetAxis("Mouse Y") * lSpeed;
            rotationX = Mathf.Clamp(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0,Input.GetAxis("Mouse X")*lSpeed,0);
        }
    }
    public Vector3 playerLocation()
    {

        return transform.position;
    }
}
