using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    Rigidbody enemy;
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;
    public bool playerInSightRange, playerInAttackRange;
    public float sightRange, attackRange;
    public GameObject projectile;
    public GameObject projectilePos;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (playerInSightRange && playerInAttackRange) Attack();
    }
    public void Attack()
    {
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
    public  void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
