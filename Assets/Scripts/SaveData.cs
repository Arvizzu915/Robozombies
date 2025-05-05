using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public TurretsPlacements turretsPlacementsData = new TurretsPlacements();

    public static SaveData singleton;

    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        string filepath = Application.persistentDataPath + "/TurretsData.json";
        string turretsData = System.IO.File.ReadAllText(filepath);

        turretsPlacementsData = JsonUtility.FromJson<TurretsPlacements>(turretsData);

        foreach (var item in turretsPlacementsData.turrets)
        {
            Instantiate(item.prefab, item.position, item.rotation);
            Debug.Log(item);
        }
    }

    public void SaveGame()
    {
        string turretsData = JsonUtility.ToJson(turretsPlacementsData);
        string filepath = Application.persistentDataPath + "/TurretsData.json";
        Debug.Log(filepath);
        System.IO.File.WriteAllText(filepath, turretsData);
    }

    public void SaveGameInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            SaveGame();
        }
    }
}

[System.Serializable]
public class TurretsPlacements
{
    public List<Turret> turrets = new List<Turret>();

    public void AddTo(GameObject turret)
    {
        Turret turretToAdd;
        turretToAdd.position = turret.transform.position;
        turretToAdd.rotation = turret.transform.rotation;
        turretToAdd.prefab = turret;
        turrets.Add(turretToAdd);
        Debug.Log("added");
        Debug.Log(SaveData.singleton.turretsPlacementsData.turrets);
    }
}

public struct Turret
{
    public GameObject prefab;
    public Vector3 position;
    public Quaternion rotation;
}

