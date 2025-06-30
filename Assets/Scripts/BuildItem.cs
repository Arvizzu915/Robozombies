using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.InputSystem.Controls;

public class BuildItem : MonoBehaviour
{
    [SerializeField] public GameObject item;
    [SerializeField] Material canBePlacedMat, canNotBePlacedMat;
    [SerializeField] MeshRenderer meshRenderer;
    


    public bool canBePlaced, onZone, overlaping;

    void Update()
    {
        //Debug.Log(canBePlaced);

        if (onZone && !overlaping)
        {
            canBePlaced = true;
            meshRenderer.material = canBePlacedMat;
        }
        else
        {
            canBePlaced = false;
            meshRenderer.material = canNotBePlacedMat;
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
            Debug.Log(other);
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
