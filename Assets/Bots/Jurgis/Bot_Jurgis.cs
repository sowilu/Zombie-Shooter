using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Jurgis : Enemy
{
    public float moveSpeed = 3.5f;
    public GameObject player;

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
