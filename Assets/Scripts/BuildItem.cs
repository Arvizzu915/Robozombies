using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildItem : MonoBehaviour
{
    [SerializeField] private GameObject item;

    private bool canBePlaced, onZone, overlaping;

    void Update()
    {
        //Debug.Log(canBePlaced);

        if (onZone && !overlaping)
        {
            canBePlaced = true;
        }
        else
        {
            canBePlaced = false;
        }
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("rotate");
            transform.Rotate(0, 0, 22.5f * context.ReadValue<Vector2>().normalized.x);
        }
    }

    public void PlaceObject(InputAction.CallbackContext callbackContext)
    {
        if (canBePlaced && callbackContext.started)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BuildZone"))
        {
            onZone = true;
        }

        if (!other.gameObject.CompareTag("BuildZone"))
        {
            overlaping = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BuildZone"))
        {
            onZone = false;
        }

        if (!other.gameObject.CompareTag("BuildZone"))
        {
            overlaping = false;
        }
    }
}
