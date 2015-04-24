using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Gun : Weapon {

    GameObject bullet_prefab;

    protected override void Start()
    {
        bullet_prefab = Resources.Load("Bullet") as GameObject;
       
        if (!bullet_prefab)
        {
            bullet_prefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet_prefab.transform.localScale = Vector3.one * .1f;
            bullet_prefab.name = "Bullet_prefab";
            bullet_prefab.AddComponent<Bullet>();
            bullet_prefab.transform.position = Camera.main.transform.position - Camera.main.transform.forward * 2f - Camera.main.transform.up*100f;          
        }
        cooldown_duration = .2f;
        base.Start();
     }
    protected override void Update()
    {
        base.Update();
    }

    protected override void Fire()
    {
        base.Fire();
        isFire = false;
        isCooldown = true;
        for (int i = 0; i < WeaponMazzles.Count; i++)
        {
            ((GameObject)Instantiate((GameObject)bullet_prefab, WeaponMazzles[i].position, WeaponMazzles[i].rotation)).GetComponent<Bullet>().Initialize(transform);
        }
        
    }
}
