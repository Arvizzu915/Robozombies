using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{   
    [SerializeField]InteractiveObject interactable = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (interactable == null && other.gameObject.CompareTag("Interactable"))
        interactable = other.gameObject.GetComponent<InteractiveObject>();
    }
    private void Update()
    {
        Interact();
    }
    void Interact()
    {
        if (interactable != null && Input.GetKeyDown(KeyCode.E))
            interactable.Interaction();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable=null;
    }
}
