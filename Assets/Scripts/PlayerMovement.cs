using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] Camera mainCamera;

    [SerializeField] float walkingSpeed, jumpForce, runningSpeed,wallrunSpeed;

    [SerializeField] CapsuleCollider standingCollider, crouchingCollider;
    [SerializeField] Transform standingHeight, crouchingHeight;
    [SerializeField] GameObject head;
    [SerializeField] Slider staminaSlider;

    private Vector3 moveDirection, localVelocity;

    private bool grounded = true, running = false, crouching = false, kneeSliding = false;
    public bool wallRunning = false, canRecover=true;
    private float velocityX, velocityZ;
    [SerializeField] private float stamina = 100f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging
        //Debug.Log(running);

        staminaSlider.value=stamina;
        //Run
        if (moveDirection.z == 0 || localVelocity.z <= 0 || stamina==0f)
        {
            running = false;
        }

        //Crouching
        if (!crouching && !kneeSliding)
        {
            head.transform.localPosition = standingHeight.localPosition;
            standingCollider.enabled = true;
            crouchingCollider.enabled = false;
        }
        else
        {
            head.transform.localPosition = crouchingHeight.localPosition;
            crouchingCollider.enabled = true;
            standingCollider.enabled = false;
        }
        if(!running && canRecover)
        { 

            stamina += 15*Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, 100);
        }
        else
        {    
            stamina -= 7.5f*Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, 100);
            if (stamina <= 0)
                StartCoroutine(Fatigue());

        }
       
    }

    private void FixedUpdate()
    {

        //Move
        Move();

        //Rotate player to camera
        RotatePlayerTowardsCamera();
    }

    //Funciones
    private void Move()
    {
        localVelocity = transform.InverseTransformDirection(rb.velocity);

        if (grounded)
        {
            if (!running && !kneeSliding)
            {
                velocityX = moveDirection.x * walkingSpeed / 1.3f;
                velocityZ = moveDirection.z * walkingSpeed;
            }
            else if (running && !kneeSliding)
            {
                velocityX = moveDirection.x * runningSpeed / 1.8f;
                velocityZ = moveDirection.z * runningSpeed;
            }
            else if (kneeSliding)
            {
                velocityX = moveDirection.x * runningSpeed / 4;
                velocityZ = runningSpeed * 1.6f;
            }
        }

        if (!grounded)
        {
            if (!running)
            {
                velocityX = moveDirection.x * walkingSpeed / 3;
                velocityZ = moveDirection.z * walkingSpeed / 1.5f;
            }
            if (running && wallRunning)
            {
                velocityZ= moveDirection.z * wallrunSpeed;
            }
            else
            {
                velocityX = moveDirection.x * runningSpeed / 3;
                velocityZ = moveDirection.z * runningSpeed / 1.5f;
            }
        }


        localVelocity.x = velocityX;
        localVelocity.z = velocityZ;

        rb.velocity = transform.TransformDirection(localVelocity);
    }

    private void RotatePlayerTowardsCamera()
    {
        if (mainCamera != null && rb != null && !kneeSliding)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f; // Ignore the y-axis rotation

            if (cameraForward != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(cameraForward);
                rb.MoveRotation(newRotation);
            }
        }
    }

    IEnumerator KneeSlide()
    {
        stamina -= 20;
        kneeSliding = true;
        yield return new WaitForSeconds(.6f);
        kneeSliding = false;
    }

    //Player Actions
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (grounded && callbackContext.performed)
        {
            kneeSliding = false;
            crouching = false;
            rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            moveDirection = callbackContext.ReadValue<Vector3>().normalized;
        }
    }

    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (grounded && callbackContext.performed && stamina >0)
        {
            running = true;
        }
    }

    public void Crouch(InputAction.CallbackContext callbackContext)
    {
        if (grounded && callbackContext.performed && !running)
        {
            crouching = true;
        }
        else if (grounded && callbackContext.performed && running)
        {
            StartCoroutine("KneeSlide");
        }

        if (grounded && callbackContext.canceled)
        {
            crouching = false;
        }
    }
    //Stamina
    IEnumerator Fatigue() 
    {
        canRecover = false;
         yield return new WaitForSeconds(2f);
        canRecover = true;
        stamina = 5;
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
