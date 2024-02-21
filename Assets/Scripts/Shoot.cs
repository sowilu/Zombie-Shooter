using UnityEngine;
using UnityEngine.Events;

public class Shoot : MonoBehaviour
{
    public UnityEvent onShoot;

    public float cooldown = 0.5f;

    float lastShot;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastShot + cooldown)
        {
            lastShot = Time.time;
            onShoot.Invoke();
        }
    }
}
