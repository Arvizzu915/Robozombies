using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public void Interaction()
    {
        this.transform.parent.position+=new Vector3(0, 20, 0);
        Debug.Log("monke");
    }
}
