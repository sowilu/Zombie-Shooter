using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour
{
    public AudioClip gunshot;
    public GameObject bulletHole;
    public GameObject muzzleFlash;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(gunshot);
        muzzleFlash.SetActive(true);
        Invoke(nameof(TurnOffMuzzleFlash), 0.03f);

        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray, out var hit))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().Die();
            }
            else
            {
                Instantiate(bulletHole , hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
            }
        }
    }
    
    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
}
