using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InteractableScript : MonoBehaviour
{   
    [SerializeField]InteractiveObject interactable = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (interactable == null && other.gameObject.CompareTag("Interactable"))
        interactable = other.gameObject.GetComponent<InteractiveObject>();
    }
   
    public void Interact(InputAction.CallbackContext callbackContext)
    {
        if (interactable != null && callbackContext.performed)
            interactable.Interaction();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable=null;
    }
}
