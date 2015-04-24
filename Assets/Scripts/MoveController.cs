using UnityEngine;
using System.Collections;

public enum MoveType
{
    Math,
    Physic
}

public class MoveController : MonoBehaviour {
    protected float speed = .03f;
    protected float acceleration = 0f;
    protected float inertion = 0f;
    protected float rotate_speed = 100f;
    protected Vector3 move_direction = Vector3.zero;
    protected Vector3 inertion_direction = Vector3.zero;
    public MoveType MoveType = MoveType.Math;
	// Use this for initialization
    virtual protected void Start()
    {
        
	}
	
	// Update is called once per frame
	virtual protected void Update () {
        if (MoveType != global::MoveType.Math) return;
        acceleration -= Time.deltaTime * speed / 2f;
        if (acceleration < 0)
        {
            acceleration = 0;
        }
        
        inertion -= Time.deltaTime * speed / 2f;
        if (inertion < 0)
        {
            inertion = 0;
        }
	}

    virtual protected void FixedUpdate()
    {
        if (MoveType != global::MoveType.Physic) return;
    }
}
