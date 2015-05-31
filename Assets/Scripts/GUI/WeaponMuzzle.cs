using UnityEngine;
using System.Collections;


public enum WeaponType
{
    None,
    Gun,
    Laser
}

public class WeaponMuzzle : MonoBehaviour {
    public WeaponType WeaponType = WeaponType.None;
}
