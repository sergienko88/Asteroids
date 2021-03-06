﻿using UnityEngine;
using System.Collections;

public class SpaceShipMoveController : MoveController
{

    protected override void Start()
    {
        base.Start();
    }
	// Update is called once per frame
    override protected void Update()
    {
        if (Time.timeScale == 0f) return;
        if (MoveType != global::MoveType.Math) return;
        //Debug.DrawRay(transform.position, transform.up);
        base.Update();

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.forward * Time.deltaTime * rotate_speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.forward * Time.deltaTime * -rotate_speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            acceleration += Time.deltaTime * speed;
            move_direction = transform.up;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            inertion = acceleration;
            inertion_direction = transform.up;
            acceleration = 0;            
        }
       
        transform.position += inertion_direction * inertion+ move_direction*acceleration;
    }

    override protected void FixedUpdate()
    {
        if (MoveType != global::MoveType.Physic) return;
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().MoveRotation(GetComponent<Rigidbody2D>().rotation + rotate_speed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().MoveRotation(GetComponent<Rigidbody2D>().rotation - rotate_speed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up );            
        }
        Debug.DrawRay(transform.position, GetComponent<Rigidbody2D>().velocity);
        //CheckSpaceCameraBorder();
    }
}
