using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public TurretsPlacements turretsPlacementsData = new TurretsPlacements();

    public static SaveData singleton;

    private void Awake()
    {
        if (singleton == null)
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

        if (System.IO.File.Exists(filepath))
        {
            string turretsData = System.IO.File.ReadAllText(filepath);
            TurretsPlacements loadedData = JsonUtility.FromJson<TurretsPlacements>(turretsData);

            if (loadedData != null && loadedData.turrets != null)
            {
                turretsPlacementsData = loadedData;

                foreach (var item in turretsPlacementsData.turrets)
                {
                    GameObject prefab = Resources.Load<GameObject>("Turrets/" + item.prefabName);
                    if (prefab != null)
                    {
                        Instantiate(prefab, item.position, item.rotation);
                    }
                    else
                    {
                        Debug.LogWarning("No se encontró el prefab: " + item.prefabName);   
                    }
                }
            }
            else
            {
                Debug.LogWarning("El archivo de datos está vacío o mal formado.");
            }
        }
        else
        {
            Debug.Log("No se encontró archivo de guardado, se empezará sin torretas.");
        }
    }

    public void ClearTorrets(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            turretsPlacementsData.Clear();
        }
    }

    public void SaveGame()
    {
        string turretsData = JsonUtility.ToJson(turretsPlacementsData, true);
        string filepath = Application.persistentDataPath + "/TurretsData.json";
        System.IO.File.WriteAllText(filepath, turretsData);
        Debug.Log("Guardado en: " + filepath);
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
        Turret turretToAdd = new Turret();
        turretToAdd.position = turret.transform.position;
        turretToAdd.rotation = turret.transform.rotation;

        // Quitar "(Clone)" del nombre
        string rawName = turret.name;
        if (rawName.EndsWith("(Clone)"))
        {
            rawName = rawName.Replace("(Clone)", "").Trim();
        }

        turretToAdd.prefabName = rawName;

        turrets.Add(turretToAdd);
        Debug.Log("Torreta añadida: " + turretToAdd.prefabName);
    }

    public void Clear()
    {
        turrets.Clear();
    }
}


[System.Serializable]
public class Turret
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
}
