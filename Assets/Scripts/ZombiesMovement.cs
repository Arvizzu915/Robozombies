using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiesMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] MeshRenderer zombieRenderer;
    private int speed;


    public float waitTime, timeReference = 0;


    // Start is called before the first frame update
    void Start()
    {
        agent.speed = 0;
        timeReference = Time.time;
        player = GameObject.Find("Player").transform;
        speed = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeReference > waitTime)
        {
            agent.speed = speed;
            zombieRenderer.enabled = true;
        }

        agent.destination = player.position;
    }
}
