using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawns = new List<Transform>();


    private void OnTriggerEnter(Collider other)
    {
        RoundsManager.roundsScript.availableSpawns = spawns;
        foreach (var zombie in RoundsManager.roundsScript.zombiesActive)
        {
            if(zombie != null)
            {
                zombie.gameObject.GetComponent<ZombiesMovement>().ChangeSpawn();
            }
        }
    }
}
