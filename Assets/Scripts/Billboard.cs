using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform target;

    void Start()
    {
        target = Camera.main.transform;
    }

   
    void Update()
    {
        transform.LookAt(transform.position + target.rotation * Vector3.forward, target.rotation * Vector3.up);
    }
}
