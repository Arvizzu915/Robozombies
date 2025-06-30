using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ballista : MonoBehaviour
{
    [SerializeField] private float shootRate = 0;
    [SerializeField] private float damage, shotSpeed;
    [SerializeField] private GameObject projectiles;

    
    private float shootTimer = 0;

    public List<OrcMovement> zombies = new();

    private void Update()
    {
        if (Time.time - shootTimer >= shootRate && zombies.Count > 0)
        {
            // Vector3 lookatpos = new Vector3((zombies[0].transform.position.x, 0, zombies[0].transform.position.z)
            transform.LookAt(zombies[0].transform.position);
            transform.Rotate(-90,0,0);
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectiles, transform.position, transform.rotation);
        newProjectile.GetComponent<ballistaProjectile>().damage = damage;
        newProjectile.GetComponent<Rigidbody>().AddForce(shotSpeed * Time.deltaTime * newProjectile.transform.forward, ForceMode.Impulse);

        shootTimer = Time.time;
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
