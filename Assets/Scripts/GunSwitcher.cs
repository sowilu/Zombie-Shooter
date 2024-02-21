using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    public GameObject[] guns;

    private int currentIndex = 0;

    
    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll > 0)
        {
            SwitchGun((currentIndex + 1) % guns.Length);
        }
        else if(scroll < 0)
        {
            var newIndex = currentIndex - 1;

            if(newIndex < 0)
                newIndex = guns.Length - 1;

            SwitchGun(newIndex);
        }
    }

    void SwitchGun(int newIndex)
    {
        guns[currentIndex].SetActive(false);

        guns[newIndex].SetActive(true);

        currentIndex = newIndex;
    }
}
