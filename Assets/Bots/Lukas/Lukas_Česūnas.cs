using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Lukas_Česūnas : Enemy
{
    private Transform target;
    public NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
       GameObject go =  GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        float distance = Vector3.Distance (transform.position, target.position);

        if (distance < 2){
            string level = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(level);
    }
}
}
