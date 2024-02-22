using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class Shoot : MonoBehaviour
{
    public TextMeshProUGUI ammoBox;
    
    public float cooldown = 0.05f;
    private float lastShot;

    public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo = value;
            ammoBox.text = $"Ammo: {ammo}";
        }
    }
    public UnityEvent onShoot;
    private int ammo = 10;
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > lastShot + cooldown && Ammo > 0)
        {
            Ammo--;
            lastShot = Time.time;
            onShoot.Invoke();
        }
        else if (Ammo == 0)
        {
            //TODO: empty glock sound
        }
    }
}
