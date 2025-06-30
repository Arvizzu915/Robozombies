using UnityEngine;

public class Slow : MonoBehaviour
{
    DamageTypes type = DamageTypes.slow;
    [SerializeField] private float speedSlow, speedNormal;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<OrcMovement>(out OrcMovement orc))
        {
            orc.TakeDamage(speedSlow, type);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<OrcMovement>(out OrcMovement orc))
        {
            orc.TakeDamage(speedNormal, type);
        }
    }
}
