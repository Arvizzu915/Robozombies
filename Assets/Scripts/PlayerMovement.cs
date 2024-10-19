using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float walkingSpeed, jumpForce, runningSpeed;

    private Vector3 moveDirection, localVelocity;

    private bool grounded = true, running = false;
    private float velocityX, velocityZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging
        //Debug.Log(running);


        //Run
        if (moveDirection.z == 0 || localVelocity.z <= 0)
        {
            running = false;
        }
    }

    private void FixedUpdate()
    {

        //Walk
        localVelocity = transform.InverseTransformDirection(rb.velocity);

        if (grounded && !running)
        {
            velocityX = moveDirection.x * walkingSpeed / 2;
            velocityZ = moveDirection.z * walkingSpeed;
        }
        else if (grounded && running) 
        {
            velocityX = moveDirection.x * runningSpeed / 3;
            velocityZ = moveDirection.z * runningSpeed;
        }
        
        if (!grounded && !running)
        {
            velocityX = moveDirection.x * walkingSpeed / 3;
            velocityZ = moveDirection.z * walkingSpeed / 1.5f;
        }
        else if (!grounded && running)
        {
            velocityX = moveDirection.x * runningSpeed / 3;
            velocityZ = moveDirection.z * runningSpeed / 1.5f;
        }


        localVelocity.x = velocityX;
        localVelocity.z = velocityZ;

        rb.velocity = transform.TransformDirection(localVelocity);

    }

    //Player Actions
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (grounded && callbackContext.performed)
        {
            rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            moveDirection = callbackContext.ReadValue<Vector3>();
        }
    }

    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (grounded && callbackContext.performed)
        {
            running = true;
        }
    }

    //Collisions
    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
