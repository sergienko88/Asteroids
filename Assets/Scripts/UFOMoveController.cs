using UnityEngine;
using System.Collections;

public class UFOMoveController : MoveController {
    Transform target = null;
    float min_distance_to_target = 4;
	// Use this for initialization
	override protected void Start () {
        base.Start();
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        if (playerShip)
        {
            target = playerShip.transform;
        }
        speed = .006f;
        rotate_speed = 500f;
        min_distance_to_target = Random.Range(4f, 5f);
	}

	// Update is called once per frame
	override protected void Update () {
        if (Time.timeScale == 0f) return;
        if (!target) return;
        base.Update();
        acceleration += Time.deltaTime * speed;
        inertion += Time.deltaTime * speed;
        move_direction = target.position - transform.position;
        inertion_direction = move_direction.normalized;        
        if (Vector3.Distance(transform.position, target.position) > min_distance_to_target)
        {
            transform.position += inertion_direction * inertion + move_direction.normalized * acceleration;
        }
        else
        {
            acceleration = 0;
            inertion = 0;
        }
	}
}
