using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieBot : Enemy
{
    private Animator animator;
    private Transform player;
    private float moveSpeed = 3f;
    private float chaseSpeed = 4f;
    private float rotationSpeed = 3f;
    private float detectionRange = 20f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(RandomMovement());
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 10f;
            randomDirection.y = 0f;

            Vector3 destination = transform.position + randomDirection;

            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                animator.SetBool("Walking", true);

                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                Quaternion targetRotation = Quaternion.LookRotation(destination - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                yield return null;
            }

            animator.SetBool("Walking", false);

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            animator.SetBool("Walking", true);
            MoveTowardsPlayer();
        }
        else
        {
            if (!IsInvoking("RestartRandomMovement"))
            {
                InvokeRepeating("RestartRandomMovement", Random.Range(1f, 3f), Random.Range(1f, 3f));
            }
        }

        if (distanceToPlayer <= 10f)
        {
            animator.SetFloat("SpeedMultiplier", 1.5f);
            MoveTowardsPlayer();
        }
        else
        {
            animator.SetFloat("SpeedMultiplier", 1f);
        }
    }

    private void MoveTowardsPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    private void RestartRandomMovement()
    {
        StopAllCoroutines();
        StartCoroutine(RandomMovement());
    }

    public override void Die()
    {
        base.Die();
    }
}
