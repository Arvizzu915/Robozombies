using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowMine : MonoBehaviour
{
    [SerializeField] GameObject mine;
    [SerializeField] Transform gun;

    public GameObject currentMine;

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentMine == null)
            {
                currentMine = Instantiate(mine, transform.position, gun.rotation);
            }
            else
            {
                currentMine.GetComponent<mina>().DetonateMine();
            }
        }
    }
}
