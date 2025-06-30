using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcMovement : MonoBehaviour
{
    protected Transform goal;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected GameObject orcRenderer;
    [SerializeField] protected Collider orcCollider, attackCollider;
    [SerializeField] protected float health, damage;
    [SerializeField] protected int speed;
    [SerializeField] protected Rigidbody rb;

    private bool attacking = false, appeared = false;

    public float waitTime, timeReference = 0;

    public int spawnIndex = 0;

    // Start is called before the first frame update
  public virtual void Start()
    {
        RoundsManager.roundsScript.zombiesActive.Add(this);
        agent.speed = 0;
        timeReference = Time.time;
        goal = GameObject.Find("Goal").transform;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - timeReference > waitTime && !attacking)
        {
            appeared = true;
            agent.speed = speed;
            orcRenderer.SetActive(true);
            orcCollider.enabled = true;
            attackCollider.enabled = true;
        }

        agent.destination = goal.position;

        if (health <= 0)
        {
            
            Destroy(gameObject);
        }
    }

    //funciones
    public virtual void TakeDamage(float damage,DamageTypes type)
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

        if (other.TryGetComponent<Base>(out Base baseScript))
        {
            baseScript.TakeDamage(damage);
        }
    }

    
}

public enum DamageTypes
{
    spike,
    push,
    slow,
    piercing,
    explosive
}
