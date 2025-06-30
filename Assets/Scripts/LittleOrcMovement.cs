using UnityEngine;

public class LittleOrcMovement : OrcMovement
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public override void TakeDamage(float damage, DamageTypes type)
    {
        if (type== DamageTypes.explosive)
        {
            health -= damage * 2;
        }
        else if (type==DamageTypes.push)
        {
            health -= damage;
            rb.AddForce(Vector3.back);
        }
        else
        {
            health -= damage;
        }
    }
}
