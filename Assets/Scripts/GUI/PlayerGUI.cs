using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class PlayerGUI : MonoBehaviour {
    public List<GUIWeaponElement> weaponElements = new List<GUIWeaponElement>();
    public WeaponController weaponController;
	// Use this for initialization
	void Start () {
        GameManager.ChangeGameStatus += (state) => {
            gameObject.SetActive(state == GameState.Play);
        };
        weaponElements = GetComponentsInChildren<GUIWeaponElement>().ToList();
        if (weaponController) {

            for (int i = 0; i < weaponController.weapons.Count; i++)
            {
                if (i <= weaponElements.Count)
                {
                    weaponElements[i].SetWeapon(weaponController.weapons[i]);                    
                }
                weaponElements[0].WeaponChange(weaponController.weapons[0]);
            }
            
        }
        gameObject.SetActive(false);
	}

}
