using System.Collections.Generic;
using UnityEngine;

public class DetonateMine : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(5f, transform.position, 10f);
        }
    }
}
