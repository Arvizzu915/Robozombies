using UnityEngine;

public class BigOrcMovement : OrcMovement
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    public override void TakeDamage(float damage, DamageTypes type)
    {
        if (type== DamageTypes.spike)
        {
            health -= damage * 2;
        }
        else
        {
            health -= damage;
        }
    }
}
