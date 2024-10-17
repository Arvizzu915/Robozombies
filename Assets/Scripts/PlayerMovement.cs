using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float walkingSpeed, jumpForce, desacceleration;

    private Vector3 moveDirection;

    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        //Walk
        if (grounded && (MathF.Abs(rb.velocity.x) < 5f && MathF.Abs(rb.velocity.z) < 5f))
        {
            rb.AddForce(moveDirection * walkingSpeed, ForceMode.Acceleration);
        }


        if (grounded && moveDirection.x == 0f)
        {
            if (rb.velocity.x != 0f) 
            {
                rb.AddForce(rb.velocity.x * desacceleration, 0f, 0f, ForceMode.Acceleration);
            }
        }

        if (grounded && moveDirection.z == 0f)
        {
            if (rb.velocity.z != 0f)
            {
                rb.AddForce(0f, 0f, rb.velocity.z * desacceleration, ForceMode.Acceleration);
            }
        }
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
        if (grounded && callbackContext.performed)
        {
            moveDirection = callbackContext.ReadValue<Vector3>();
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
