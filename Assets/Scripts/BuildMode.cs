using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class BuildMode : MonoBehaviour
{
    

    [SerializeField] private GameObject buildableActive;

    [SerializeField] private GameObject buildUI;

    [SerializeField] private GameObject constructionZones;

    [SerializeField] private GameObject[] itemPreview;

    bool inBuildMode = false;

    

    public void EnterBuildMode(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inBuildMode)
            {
                buildableActive.SetActive(false);
                buildUI.SetActive(false);
                constructionZones.SetActive(false);
                inBuildMode = false;
            }
            else
            {
                buildableActive.SetActive(true);
                buildUI.SetActive(true);
                constructionZones.SetActive(true);
                inBuildMode = true;
            }
            
        }
    }

    public void PlaceObject(InputAction.CallbackContext callbackContext)
    {
        if (buildableActive.GetComponent<BuildItem>().canBePlaced && callbackContext.started)
        {
            Debug.Log("place");
            SaveData.singleton.turretsPlacementsData.AddTo(Instantiate(buildableActive.GetComponent<BuildItem>().item, buildableActive.transform.position, buildableActive.transform.rotation));
        }
        Debug.Log("no");
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("rotate");
            transform.Rotate(0, 45, 0);
        }
    }

    void SelectBuildable(int index)
    {
        for (int i = 0; i < itemPreview.Length; i++)
        {
            itemPreview[i].SetActive(false);
        }

        buildableActive = itemPreview[index];

        if (inBuildMode)
        {
            itemPreview[index].SetActive(true);
        }
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        // Detect which key was pressed
        var keyControl = context.control as KeyControl;
        if (keyControl == null) return;

        switch (keyControl.keyCode)
        {
            case Key.Digit1:
                SelectBuildable(0);
                break;
            case Key.Digit2:
                SelectBuildable(1);
                break;
            case Key.Digit3:
                SelectBuildable(2);
                break;
        }
    }
}
