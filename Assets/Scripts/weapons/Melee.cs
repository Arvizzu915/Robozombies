using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float damage;

    private void Attack()
    {
        animator.Play("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        ZombiesMovement zombieScript = other.GetComponent<ZombiesMovement>();

        zombieScript?.TakeDamage(damage);

        Destroy(gameObject);
    }

    public void MeleeInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Attack();
        }
    }
}
