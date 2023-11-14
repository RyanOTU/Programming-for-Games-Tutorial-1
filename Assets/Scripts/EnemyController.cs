using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    Rigidbody enemy;
    
    //Walking
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;
    public float speed = 5;
    
    //Attacking
    public GameObject projectile;
    public GameObject projectilePos;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public bool playerInSightRange, playerInAttackRange;
    public float sightRange, attackRange;

    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (playerInSightRange && playerInAttackRange) Attack();
        if (!playerInSightRange) Patrol();
        if (playerInSightRange)
        {
            //Debug.Log("Fear the old blood");
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    public void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, projectilePos.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceWalkPoint = transform.position - walkPoint;
        if (distanceWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void Chase()
    {
        agent.SetDestination(player.position);
    }
    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Physics.Raycast(walkPoint, -transform.up, 1f, groundLayer)) ;
    }
}
