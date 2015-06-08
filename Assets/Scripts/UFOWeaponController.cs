using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UFOWeaponController : WeaponController {
    float attack_time = 1f;    
    List<Transform> muzzles = new List<Transform>();
    Transform target = null;
	// Use this for initialization
	void Start () {
        FindTarget();
        weapons.Add(gameObject.AddComponent<Gun>());
        selected_weapon_index = 0;
        System.Array.ForEach(transform.root.GetComponentsInChildren<WeaponMuzzle>(),m=>{
            muzzles.Add(m.transform);        
        });
        Invoke("Attack", attack_time);
	}

    void FindTarget()
    {
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        if (playerShip)
        {
            target = playerShip.transform;
        }
    }

    void Attack()
    {
        if (!target)
        {
            FindTarget();
        }
        else
        {
            if (target.GetComponent<SpaceObject>().enabled&&target.gameObject.activeSelf)
            {
                weapons.ForEach(w => w.Shot());
            }
        }
        attack_time = Random.Range(2f, 4f);
        Invoke("Attack", attack_time);
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
    }
}
