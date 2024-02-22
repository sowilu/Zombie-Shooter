using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    public Animator animator;

    public float triggerDistance = 10;
    public float forgerDistance = 30;

    public float chasingSpeed = 4;

    bool isFollowing;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude);

        var distance = Vector3.Distance(transform.position, target.position);

        if (distance <= triggerDistance)
        {
            isFollowing = true;
            agent.speed = chasingSpeed;
        }

        else if (distance <= forgerDistance)
        {
            isFollowing = false;

        }

        if (isFollowing)
        {
            agent.SetDestination(target.position);
        }
    }
}
