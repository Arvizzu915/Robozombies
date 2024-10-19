using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float sensitivity;

    private Vector2 mouseInput;

    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, mouseInput.x * sensitivity * Time.deltaTime);

        pitch -= mouseInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        transform.localEulerAngles = new Vector3 (pitch, transform.localEulerAngles.y, 0);
    }

    public void GetMouseInput(InputAction.CallbackContext callbackContext)
    {
        mouseInput = callbackContext.ReadValue<Vector2>();
    }
}
