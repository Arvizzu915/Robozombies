using UnityEngine;
using UnityEngine.InputSystem;

public class AOE : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float throwForce;

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        newProjectile.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }

    public void ShootInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Shoot();
        }
    }
}
