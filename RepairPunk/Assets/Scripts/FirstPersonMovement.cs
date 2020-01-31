using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{

    public CharacterController controller;

    [Header("Movement")]
    public float walkSpeed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public float jumpForce = 5000.0f;
    public float sprintSpeed = 15f;

    [Header("GroundChecks")]
    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask;
    bool isGrounded;



    //private Vector3 moveDirection = Vector3.zero;



    void Update()
    {
        Debug.Log(isGrounded);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * walkSpeed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumped");
        }
            
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
    }
    
}
 
