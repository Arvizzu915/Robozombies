using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    [SerializeField] public List<Transform> availableSpawns = new List<Transform>();

    [SerializeField] GameObject zombie;

    public List<GameObject> zombiesActive = new List<GameObject>();

    public int round = 1;

    public int numberOfZombiesInRound = 10;

    public int numberOfZombiesAlive = 0;

    public int maxNumberOfZombiesAlive = 15;

    public int numberOfZombiesKilled = 0;

    public int numberOfZombiesSpawned = 0;

    public bool roundWon = false;

    public bool normalRound = true;


    private Transform spawn;
    private int spawnIndex;

    public static RoundsManager roundsScript;

    private void Start()
    {
        roundsScript = this;
    }

    private void Update()
    {
        if (normalRound)
        {
            if (numberOfZombiesKilled == numberOfZombiesInRound && !roundWon)
            {
                roundWon = true;
                WinRound();
            }

            if (numberOfZombiesSpawned < numberOfZombiesInRound && numberOfZombiesAlive < maxNumberOfZombiesAlive)
            {
                spawnIndex = Random.Range(0, availableSpawns.Count);
                numberOfZombiesSpawned++;
                numberOfZombiesAlive++;
                Instantiate(zombie, availableSpawns[spawnIndex].position, availableSpawns[spawnIndex].rotation);
                float time = Random.Range(10, 25);
                zombie.gameObject.GetComponent<ZombiesMovement>().waitTime = time;
            }
        }
    }

    public void WinRound()
    {
        round++;
        numberOfZombiesInRound += 5;
        numberOfZombiesKilled = 0;
        roundWon = false;
    }
}
