using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpaceShipWeaponController : WeaponController
{
    protected override void Awake()
    {
        base.Awake();
        AddWeapon(WeaponType.Gun);
        AddWeapon(WeaponType.Laser);        
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("1"))
        {
            selected_weapon_index = 0;
            WeaponChanged(weapons[selected_weapon_index]);
        }
        
        if (Input.GetKeyDown("2"))
        {
            selected_weapon_index = 1;
            WeaponChanged(weapons[selected_weapon_index]);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selected_weapon_index!=-1)
                weapons[selected_weapon_index].Shot();
        }
	}
}