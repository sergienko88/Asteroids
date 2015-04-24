using UnityEngine;
using System.Collections;

public class SpaceObject : MonoBehaviour {

    static int objectCount = 0;
    static int maxObjectCount = 0;

    public virtual int ObjectCount
    {
        get { return SpaceObject.objectCount; }
        set { SpaceObject.objectCount = value; }
    }
    
    public virtual int MaxObjectCount
    {
        get { return SpaceObject.maxObjectCount; }
        set { SpaceObject.maxObjectCount = value; }
    }


    public static System.Action<System.Type> ObjectDestroyed;
    static SpaceObject()
    {
        ObjectDestroyed += (type) => { };
    }

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
	
	}

    public virtual bool Damage()
    {
        ObjectDestroyed(GetType());
        objectCount = Mathf.Clamp(--objectCount, 0, maxObjectCount);
        return true;
    }

    public virtual bool Damage(Transform Owner)
    {
        return true;
    }
}
