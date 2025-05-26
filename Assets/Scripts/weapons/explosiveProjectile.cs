using UnityEngine;

public class explosiveProjectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionCollider;
    [SerializeField] private float damage;


    private void OnCollisionEnter(Collision collision)
    {
        explosionCollider.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        ZombiesMovement zombieScript = other.GetComponent<ZombiesMovement>();

        zombieScript?.TakeDamage(damage);

        Destroy(gameObject);
    }
}
