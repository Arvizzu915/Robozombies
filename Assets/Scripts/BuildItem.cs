using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildItem : MonoBehaviour
{
    [SerializeField] private GameObject item;

    private bool canBePlaced, onZone, overlaping;

    void Update()
    {
        Debug.Log(canBePlaced);

        if (onZone && !overlaping)
        {
            canBePlaced = true;
        }
        else
        {
            canBePlaced = false;
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
