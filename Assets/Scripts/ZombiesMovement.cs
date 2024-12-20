using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiesMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] MeshRenderer zombieRenderer;
    [SerializeField] Collider zombieCollider, attackCollider;
    [SerializeField] float health, damage;
    private int speed;

    private bool attacking = false, appeared = false;

    public float waitTime, timeReference = 0;

    public int spawnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        RoundsManager.roundsScript.zombiesActive.Add(gameObject);
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
            appeared = true;
            agent.speed = speed;
            zombieRenderer.enabled = true;
            zombieCollider.enabled = true;
            attackCollider.enabled = true;
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

    public void ChangeSpawn()
    {
        if (!appeared)
        {
            spawnIndex = Random.Range(0, RoundsManager.roundsScript.availableSpawns.Count);
            transform.position = RoundsManager.roundsScript.availableSpawns[spawnIndex].position;
        }
        
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
