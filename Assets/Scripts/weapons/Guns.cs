using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class Guns : MonoBehaviour
{
    [SerializeField] Transform muzzle;

    [SerializeField] float firerate, damage, reloadTime, distance, magSize, bulletsLeft;
    [SerializeField] bool automatic;
    private bool canShoot = true, shooting = false;

    public Vector3 spread = new Vector3(.05f, .05f, .05f);

    private Vector3 spreadDirection = Vector3.zero;
    Vector3 shootDirection;

    private void Start()
    {
        bulletsLeft = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletsLeft <= 0)
        {
            canShoot = false;
        }

        if (shooting && canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("shoot");

        bulletsLeft--;
        canShoot = false;
        spreadDirection = new Vector3(Random.Range(-spread.x, spread.x), Random.Range(-spread.y, spread.y), Random.Range(-spread.z, spread.z));
        shootDirection = muzzle.forward + spreadDirection;

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, shootDirection, out hit, distance))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                ZombiesMovement zombieScript = hit.transform.gameObject.GetComponent<ZombiesMovement>();
                zombieScript.TakeDamage(damage);
            }
        }

        StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(firerate);
        canShoot = true;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magSize;
        canShoot = true;
    }

    public void ShootNoAuto(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started && canShoot && automatic)
        {
            Shoot();
        }
    }

    public void ShootAuto(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        { 
            shooting = true;
        }

        if (callbackContext.canceled)
        {
            shooting = false;
        }
    }

    public void ReloadInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && bulletsLeft < magSize) 
        {
            StartCoroutine(Reload());
        }
    }
}
