using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UFOWeaponController : WeaponController {
    float shot_rate = .02f;
    float random = 0f;
    List<Transform> muzzles = new List<Transform>();
    Transform target = null;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        weapons.Add(gameObject.AddComponent<Gun>());
        selected_weapon_index = 0;
        System.Array.ForEach(transform.root.GetComponentsInChildren<WeaponMuzzle>(),m=>{
            muzzles.Add(m.transform);        
        });
	}

    void Update()
    {
        if (target)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion l = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 0, 90*(dir.x>=0?-1:1));
            l.x = 0;
            l.y = 0;
            muzzles.ForEach(m => m.rotation=l);
        }
        random = Random.value;
        if (random < shot_rate)
        {            
            weapons.ForEach(w => w.Shot());
        }
    }
	

}
