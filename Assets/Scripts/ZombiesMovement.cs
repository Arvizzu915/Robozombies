using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiesMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent.speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }
}
