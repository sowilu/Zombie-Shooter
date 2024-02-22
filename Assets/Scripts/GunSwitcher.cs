using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    public GameObject[] guns;
    private int currentGun = 0;
    
    void Start()
    {
        //disable all guns except the first one
        for (int i = 1; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[0].SetActive(true);
    }

    
    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0)
        {
            SwitchGun((currentGun + 1) % guns.Length);
        }
        else if (scroll < 0)
        {
            var newGun = currentGun - 1;
            
            if(newGun < 0) newGun = guns.Length - 1;
            
            SwitchGun(newGun);
        }
    }
    
    void SwitchGun(int newGun)
    {
        guns[currentGun].SetActive(false);
        guns[newGun].SetActive(true);
        currentGun = newGun;
    }
}
