using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public GameObject scrap;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject ground;
    public float health;
    public Animator animator;

    //sounds
    public AudioSource hurtAudio;
    public AudioSource deathAudio;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool dead = false;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        walkPointSet = false;
        animator = GetComponentInChildren<Animator>();
        var aSources = GetComponents<AudioSource>();
        deathAudio = aSources[1];
        hurtAudio = aSources[2]; 
        
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void Patroling()
    {
        animator.SetBool("Walking", true);
        GetComponent<NavMeshAgent>().speed = 1.5f;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 5f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        walkPointSet = true;
    }
 
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("Walking", false);
        GetComponent<NavMeshAgent>().speed = 6.0f;
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        animator.SetBool("Walking", false);
        GetComponent<NavMeshAgent>().speed = 6.0f;
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Vector3 pos = new Vector3(transform.position.x , transform.position.y, transform.position.z);
            Rigidbody rb = Instantiate(projectile, pos, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
            rb.AddForce(transform.up * 2f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (dead == false)
        {
            hurtAudio.Play();
            if (health <= 0) 
            {
                StartCoroutine(DestroyEnemy());
            }
        }
        
        
    }
    IEnumerator DestroyEnemy()
    {
        
        if(dead == false)
        {
            dead = true;
            deathAudio.Play();
            yield return new WaitForSeconds(2.4f);
            
            if (gameObject.activeSelf == true) 
            {
                
                // Collider m_Collider = GetComponent<Collider>();
                // m_Collider.enabled = !m_Collider.enabled;
                Vector3 pos = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 1.5f, gameObject.transform.position.z);
                Vector3 posOne = new Vector3(gameObject.transform.position.x + -0.5f, gameObject.transform.position.y + +1.0f, gameObject.transform.position.z + 1.45f); 
                GameObject.Instantiate(scrap, pos, gameObject.transform.rotation);
                GameObject.Instantiate(scrap, posOne, gameObject.transform.rotation);
                gameObject.SetActive(false);
            }
            
        }
        
        
    }

    public void OnTriggerEnter(Collider col)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if(col.gameObject.name.Contains ("PlayerCube Variant"))
        {
            TakeDamage(10);
            Destroy(col.gameObject);
        }
        
        
     
    }
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, attackRange);
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, sightRange);
    // }
}
