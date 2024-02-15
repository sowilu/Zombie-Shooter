using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shoot : MonoBehaviour
{
    public float cooldown = 0.5f;

    public AudioClip gunshot;
    public GameObject muzzleFlash;
    public GameObject bulletHole;

    AudioSource audioSource;
    float lastShot;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastShot + cooldown)
        {
            audioSource.PlayOneShot(gunshot);
            muzzleFlash.SetActive(true);
            Invoke(nameof(TurnOffMuzzleFlash), 0.1f);
            lastShot = Time.time;

            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if(Physics.Raycast(ray, out var hitInfo))
            {
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    hitInfo.transform.GetComponent<Enemy>().Die();
                }
                else
                {
                    Instantiate(bulletHole, hitInfo.point + hitInfo.normal * 0.01f, Quaternion.LookRotation(hitInfo.normal));
                }
            }
        }
    }

    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
}
