using UnityEngine;
using System.Collections;

public class SpaceShip : SpaceObject {

    static int objectCount = 0;
    static int maxObjectCount = 1;

    public virtual int ObjectCount
    {
        get { return SpaceShip.objectCount; }
        set { SpaceShip.objectCount = value; }
    }

    public virtual int MaxObjectCount
    {
        get { return SpaceShip.maxObjectCount; }
        set { SpaceShip.maxObjectCount = value; }
    }

    Vector3 start_position = Vector3.zero;
	// Use this for initialization
	protected override void Start () {
        start_position = transform.position;
	}

    public override bool Damage()
    {        
        transform.position = start_position;
        ObjectDestroyed(GetType());
        objectCount = Mathf.Clamp(--objectCount, 0, maxObjectCount);
        return true;
    }

    public override bool Damage(Transform Owner)
    {     
        if (Owner != transform)
        {
            return Damage();
        }
        return false;
    }
}
