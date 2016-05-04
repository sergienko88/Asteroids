using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WeaponController : MonoBehaviour {
    public List<Weapon> weapons = new List<Weapon>();
    protected int selected_weapon_index = 0;
    public SpaceObject Owner;
    public static System.Action<Weapon> WeaponChanged;
    protected virtual void Awake() {
        WeaponChanged += (weapon)=> { };
        Owner = GetComponent<SpaceObject>();
    }

    protected void AddWeapon(WeaponType weaponType) {
        if(Owner) {
            Weapon t_weapon = null;
            switch (weaponType)
            {
                case WeaponType.None:
                    break;
                case WeaponType.Gun:
                    t_weapon = gameObject.AddComponent<Gun>();
                    t_weapon.WeaponType = WeaponType.Gun;
                    break;
                case WeaponType.Laser:
                    t_weapon = gameObject.AddComponent<Laser>();
                    t_weapon.WeaponType = WeaponType.Laser;
                    break;
                default:
                    break;
            }
            if (t_weapon!=null)
            {
                t_weapon.Owner = Owner;
                weapons.Add(t_weapon);
            }
        }
    }
}
