using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {
    public float cooldown_duration = 3f;
    public float cooldown = 0f;
    protected bool isCooldown = false;
    protected bool isFire = false;
    protected List<Transform> WeaponMazzles = new List<Transform>();
    public WeaponType WeaponType = WeaponType.None;
    public SpaceObject Owner;
	// Use this for initialization
	protected virtual void Start () {
        cooldown = cooldown_duration;
        WeaponType = GetWeaponType(this.GetType().ToString());
        WeaponMuzzle[] muzzles = System.Array.FindAll(transform.root.GetComponentsInChildren<WeaponMuzzle>(), a => a.WeaponType == WeaponType);
        for (int i = 0; i < muzzles.Length; i++)
        {
            WeaponMazzles.Add(muzzles[i].transform);
        }
        if (muzzles.Length == 0)
        {

            Transform gun_muzzle = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            gun_muzzle.name = "WeaponMuzzle";
            gun_muzzle.localScale = Vector3.one * .1f;
            Destroy(gun_muzzle.GetComponent<SphereCollider>());
            gun_muzzle.parent = transform;
            gun_muzzle.position = new Vector3(
                transform.GetChild(0).GetComponent<Renderer>().bounds.center.x,
                transform.GetChild(0).GetComponent<Renderer>().bounds.max.y,
                transform.GetChild(0).GetComponent<Renderer>().bounds.center.z
            );
            WeaponMazzles.Add(gun_muzzle);
            WeaponMazzles.ForEach(wm => wm.name = WeaponType.ToString() + "Muzzle");
        }
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (!isFire)
        {
            if (isCooldown)
            {
                if (cooldown < cooldown_duration)
                {
                    cooldown += Time.deltaTime;
                }

                if (cooldown > cooldown_duration)
                {
                    cooldown = cooldown_duration;
                    isCooldown = false;
                }
            }
        }
	}

    public void Shot()
    {
        if (!isCooldown&&!isFire)
        {
            Fire();
        }
    }

    protected virtual void Fire()
    {
        isFire = true;
        cooldown = 0;
    }

    WeaponType GetWeaponType(string type)
    {

        WeaponType wt = WeaponType.None;
        foreach (var value in System.Enum.GetNames(typeof(WeaponType)))
        {
            
            if (value == type.ToString())
            {
                wt = (WeaponType)System.Enum.Parse(typeof(WeaponType), type);
            }
        }
        return wt;
    }
}
