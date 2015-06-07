using UnityEngine;
using System.Collections;

public class SpaceObject : MonoBehaviour {

    static int objectCount = 0;
    static int maxObjectCount = 0;
    protected int RewardPoints = 0;
    protected bool isDamagedYet = false;
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


    public static System.Action<System.Type,int> ObjectDestroyed;
    static SpaceObject()
    {
        ObjectDestroyed += (type,points) => { };
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
        ObjectDestroyed(GetType(),0);
        objectCount = Mathf.Clamp(--objectCount, 0, maxObjectCount);
        isDamagedYet = true;
        return true;
    }

    public virtual bool Damage(Transform Owner)
    {
        isDamagedYet = true;
        return true;
    }
}
