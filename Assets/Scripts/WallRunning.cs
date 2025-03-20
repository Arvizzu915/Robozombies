using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float maxWallRunTime;
    public float wallRunTimer;

    [Header("Input")]
    Vector3 input;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm= GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        CheckForWall();
        StateMachine();
    }
    private void FixedUpdate()
    {
        if (pm.wallRunning)
            WallRunningMovement();
    }
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
;    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        

        //State 1
        if((wallLeft||wallRight) && input.z > 0 && AboveGround()) 
        { 
            if(!pm.wallRunning)
            {
                StartWallRun();
            }
        }
        else
        {
            if (pm.wallRunning)
            {
                StopWallRun();
            }
            
        }

    }
    private void StartWallRun()
    {
       
        pm.wallRunning = true;
    }
    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z);
        Vector3 wallNormal = wallRight ? rightWallhit.normal :leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if((orientation.forward-wallForward).magnitude>(orientation.forward- -wallForward).magnitude)
            wallForward=-wallForward;

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
        if (!(wallLeft && input.x > 0) && !(wallRight && input.x < 0))
        rb.AddForce(-wallNormal*wallRunForce, ForceMode.Force);
    }
    private void StopWallRun()
    {
        rb.useGravity = true;
        pm.wallRunning = false;
    }
    public void GetMoveInput(InputAction.CallbackContext callbackContext)
    {
        input = callbackContext.ReadValue<Vector3>();
        
    }
}
