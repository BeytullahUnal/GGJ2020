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
    public float jumpDest = 1.2f;
    public float sprintSpeed = 15f;
    public float dashForce = 5f;

    [Header("GroundChecks")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private bool isDashing;


    void Update()
    {
        //Debug.Log(isGrounded);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * walkSpeed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumped");
            //controller.Move(Vector3.up * jumpForce);
            Jump();
        }

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
        }


    }

    private void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(DashRoutine());
            isDashing = true;
        }
    }

    private IEnumerator DashRoutine()
    {
        var dashT = 0f;
        while (dashT < .2f)
        {
            dashT += Time.deltaTime;
            controller.Move(GetComponentInChildren<MouseLook>().transform.forward * dashForce * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
    }

    private void Jump()
    {
        //StartCoroutine(JumpRoutine());
        velocity.y = Mathf.Sqrt(jumpDest * -1f * gravity); 
    }

    /*private IEnumerator JumpRoutine()
    {
        var jumpT = 0f;
        while (jumpT < .3f)
        {
            jumpT += Time.deltaTime;
            controller.Move(transform.up * jumpForce * Time.deltaTime);
            yield return null;
        }
    }*/
}

