using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowMine : MonoBehaviour
{
    [SerializeField] GameObject mine;
    [SerializeField] Transform gun;

    public GameObject currentMine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.started && currentMine==null)
        {
            currentMine = Instantiate(mine, transform.position, gun.rotation);
        }
    }
}
