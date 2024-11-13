using UnityEngine;

public class slope : MonoBehaviour
{
    [SerializeField] float fuerzaNormal;

    // Start is called before the first frame update
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerMovement.playerScript.
        }
    }
}
