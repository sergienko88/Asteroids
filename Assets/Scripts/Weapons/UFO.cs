using UnityEngine;
using System.Collections;

public class UFO : SpaceObject{
    static int objectCount = 0;
    static int maxObjectCount = 5;

    public override int ObjectCount
    {
        get { return UFO.objectCount; }
        set { UFO.objectCount = value; }
    }


    public override int MaxObjectCount
    {
        get { return UFO.maxObjectCount; }
        set { UFO.maxObjectCount = value; }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool Damage()
    {
        ObjectDestroyed(GetType());
        objectCount = Mathf.Clamp(--objectCount, 0, maxObjectCount);
        Destroy(gameObject);
        return true;
    }

    public override bool Damage(Transform Owner)
    {
        if (Owner.GetComponent<SpaceShip>())
        {
           return Damage();
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SpaceObject>())
        {
            other.GetComponent<SpaceObject>().Damage(transform);
            Damage(other.transform);
        }
    }
}
