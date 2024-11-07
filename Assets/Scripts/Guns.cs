using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class Guns : MonoBehaviour
{
    [SerializeField] Transform muzzle;

    [SerializeField] float firerate, damage, reloadTime, distance, bulletNumber;
    private bool canShoot = true;

    public Vector3 spread = new Vector3(.05f, .05f, .05f);

    private Vector3 spreadDirection = Vector3.zero;
    Vector3 shootDirection;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(firerate);
        canShoot = true;
    }

    public void Shoot(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started && canShoot)
        {
            Debug.Log("shoo");

            canShoot = false;
            spreadDirection = new Vector3(Random.Range(-spread.x, spread.x), Random.Range(-spread.y, spread.y), Random.Range(-spread.z, spread.z));
            shootDirection = muzzle.forward + spreadDirection;

            RaycastHit hit;
            if (Physics.Raycast(muzzle.position, shootDirection, out hit, distance))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    ZombiesMovement zombieScript = hit.transform.gameObject.GetComponent<ZombiesMovement>();
                    zombieScript.TakeDamage(damage);
                }
            }
            
            StartCoroutine(FireRate());
        }
    }
}
