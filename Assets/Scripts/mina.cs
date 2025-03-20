using UnityEngine;
using UnityEngine.InputSystem;

public class mina : MonoBehaviour
{
    [SerializeField] private Collider explosionZone;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float throwForce;

    private void Start()
    {
        rb.AddForce(gameObject.transform.forward * throwForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
    }
}
