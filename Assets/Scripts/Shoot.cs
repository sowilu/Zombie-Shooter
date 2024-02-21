using UnityEngine;
using UnityEngine.Events;

public class Shoot : MonoBehaviour
{
    public int bullets = 10;

    public UnityEvent onShoot;

    public float cooldown = 0.5f;

    float lastShot;

    private void Start()
    {
        BulletCounter.instance.ShowBulletCount(bullets);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastShot + cooldown)
        {
            if (bullets > 0)
            {
                BulletCounter.instance.ShowBulletCount(--bullets);
                lastShot = Time.time;
                onShoot.Invoke();
            }
            else
            {
                //TODO: play empty glock sound
            }
        }
    }
}
