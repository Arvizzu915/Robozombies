using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiesMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] MeshRenderer zombieRenderer;
    [SerializeField] float health, damage;
    private int speed;

    private bool attacking = false;

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
        if (Time.time - timeReference > waitTime && !attacking)
        {
            agent.speed = speed;
            zombieRenderer.enabled = true;
        }

        agent.destination = player.position;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //funciones
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    IEnumerator Attack()
    {
        attacking = true;
        Debug.Log("attack");
        agent.speed = 0f;
        yield return new WaitForSeconds(.7f);
        PlayerMovement.playerScript.TakeDamge(damage);
        attacking = false;
    }

    //collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }
}
