using UnityEngine;
using UnityEngine.InputSystem;

public class BuildMode : MonoBehaviour
{
    [SerializeField] private GameObject buildBrain;

    [SerializeField] private GameObject buildUI;

    [SerializeField] private GameObject constructionZones;

    bool inBuildMode = false;

    public void EnterBuildMode(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inBuildMode)
            {
                buildBrain.SetActive(false);
                buildUI.SetActive(false);
                constructionZones.SetActive(false);
                inBuildMode = false;
            }
            else
            {
                buildBrain.SetActive(true);
                buildUI.SetActive(true);
                constructionZones.SetActive(true);
                inBuildMode = true;
            }
            
        }
    }
}
