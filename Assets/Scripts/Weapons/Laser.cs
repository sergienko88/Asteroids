using UnityEngine;
using System.Collections;

public class Laser : Weapon {
    LineRenderer laser_line_render;
    bool isRenderLaser = false;
    float laser_render_duration = .5f;
    float laser_render_timer = 0f;
    float laser_length = 5f;
    int current_laser_muzzle = 0;
    RaycastHit2D[] hits;
	// Use this for initialization
    protected override void Start()
    {
        cooldown_duration = 1f;
        laser_render_timer = laser_render_duration;
        base.Start();
        laser_line_render = gameObject.AddComponent<LineRenderer>();
        laser_line_render.SetWidth(.1f, .1f);
        SpaceObject.ObjectDestroyed += (owner) =>
        {
            if (this.Owner == owner)
            {                
                laser_line_render.SetPosition(0, owner.transform.position);
                laser_line_render.SetPosition(1, owner.transform.position);
            }
        };
    } 
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isRenderLaser)
        {
            laser_render_timer -= Time.deltaTime;
            if (laser_render_timer < 0f)
            {
                isRenderLaser = false;
                isFire = false;
                isCooldown = true;
                laser_line_render.enabled = false;
            }
            else
            {
                RenderLaser();
                hits = Physics2D.RaycastAll(WeaponMazzles[current_laser_muzzle].position, ((Vector2)WeaponMazzles[current_laser_muzzle].up), laser_length);
                Debug.DrawRay((Vector2)WeaponMazzles[current_laser_muzzle].position,(Vector2)WeaponMazzles[current_laser_muzzle].up*laser_length, Color.red, .1f);
                System.Array.ForEach(hits, h => {
                    SpaceObject target = h.transform.GetComponent<SpaceObject>();
                    if (target && target!=Owner)
                    {
                        target.Damage(Owner);
                    }
                });
            }
        }
	}

    void RenderLaser()
    {
        laser_line_render.SetPosition(0, WeaponMazzles[current_laser_muzzle].position);
        laser_line_render.SetPosition(1, WeaponMazzles[current_laser_muzzle].position + WeaponMazzles[current_laser_muzzle].up * laser_length);
    }

    protected override void Fire()
    {
        laser_line_render.enabled = true;
        base.Fire();
        isRenderLaser = true;
        laser_render_timer = laser_render_duration;
        current_laser_muzzle++;
        if (current_laser_muzzle >=WeaponMazzles.Count)
        {
            current_laser_muzzle = 0;
        }
    }

}
