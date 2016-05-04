using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GUIWeaponElement : MonoBehaviour {
    Button uiButton;
    Image uiImage;
    Text uiText;
    Weapon weapon;
    System.Action UpdateElement;
    public Color defaultColor = Color.white;
    public Color usedColor = Color.green;
	// Use this for initialization
	void Start () {
        uiButton = GetComponentInChildren<Button>();
        uiImage = GetComponentInChildren<Image>();
        uiText = GetComponentInChildren<Text>(); 
        UpdateElement += () => { };
    }

    public void WeaponChange(Weapon ChangedWeapon) {
        if (weapon)
        {
            uiImage.color = ChangedWeapon == weapon ? usedColor : defaultColor;
        }
    }

    public void SetWeapon(Weapon weaponToSet) {
        WeaponController.WeaponChanged -= WeaponChange;
        weapon = weaponToSet;
        uiText.text = weaponToSet.WeaponType.ToString();
        WeaponController.WeaponChanged += WeaponChange;
        UpdateElement = null;
        UpdateElement += () => { };
        UpdateElement += () =>
        {               
            uiImage.fillAmount = Mathf.Lerp(0f, 1f, weapon.cooldown / weapon.cooldown_duration);
        };       
    }

    void Update() {
        if (!weapon) return;
        UpdateElement();
    }

}
