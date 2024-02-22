using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jonas : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator enemyAnimator;

	private void Awake()
	{
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Wandering();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
	}

	private void Wandering()
    {
        if (!walkPointSet) 
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            enemyAnimator.SetBool("isRunning", false);
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
			enemyAnimator.SetBool("isRunning", true);
		}
	}

	private void ChasePlayer()
	{
        agent.SetDestination(player.position);
		enemyAnimator.SetBool("isRunning", true);
	}

	private void AttackPlayer()
	{
        agent.SetDestination(transform.position);
        transform.LookAt(player);
		enemyAnimator.SetBool("isRunning", false);

		if (!alreadyAttacked)
		{
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
			rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            Destroy(rb.gameObject, 5);


			alreadyAttacked = true;
			Invoke(nameof(ResetAttack), timeBetweenAttacks);
		}
	}
    
    private void ResetAttack()
    {
        alreadyAttacked= false;
    }



	public virtual void Die()
    {
        //TODO: fancy death animations
        Destroy(gameObject);
    }
}
