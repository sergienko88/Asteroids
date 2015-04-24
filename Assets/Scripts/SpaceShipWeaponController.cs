using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpaceShipWeaponController : WeaponController
{

	// Use this for initialization
	void Start () {
        weapons.Add(gameObject.AddComponent<Gun>());
        weapons.Add(gameObject.AddComponent<Laser>());
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("1"))
        {
            selected_weapon_index = 0;            
        }
        
        if (Input.GetKeyDown("2"))
        {
            selected_weapon_index = 1;            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selected_weapon_index!=-1)
                weapons[selected_weapon_index].Shot();
        }
	}
}