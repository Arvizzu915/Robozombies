using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageRate;
    private float damageTimer = 0;

    private DamageTypes damageType = DamageTypes.piercing;
    private List<OrcMovement> zombies = new();

    private void Update()
    {
        if (Time.time - damageTimer >= damageRate && zombies.Count > 0)
        {
            foreach (var zombie in zombies)
            {
                Damage(zombie);
            }
        }
    }

    private void Damage(OrcMovement zombie)
    {
        zombie.TakeDamage(damage, damageType);
        damageTimer = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<OrcMovement>(out OrcMovement zombieScript))
        {
            zombies.Add(zombieScript);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<OrcMovement>(out OrcMovement zombieScript))
        {
            zombies.Remove(zombieScript);
        }
    }
}
