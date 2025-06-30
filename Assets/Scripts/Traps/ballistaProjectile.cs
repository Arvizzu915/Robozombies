using UnityEngine;

public class ballistaProjectile : MonoBehaviour
{
    public float damage;
    public int orcsPiercing;

    private DamageTypes damageType = DamageTypes.piercing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<OrcMovement>(out OrcMovement orc) && orcsPiercing > 0)
        {
            orc.TakeDamage(damage, damageType);
            orcsPiercing--;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
