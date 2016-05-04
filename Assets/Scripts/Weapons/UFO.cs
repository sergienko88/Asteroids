using UnityEngine;
using System.Collections;

public class UFO : SpaceObject{
    public static int objectCount = 0;
    public static int maxObjectCount = 5;

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
    protected override void Start()
    {
        base.Start();
        RewardPoints = 50;
    }

    public override bool Damage()
    {
        Destroy(gameObject);
        return true;
    }

    public override bool Damage(SpaceObject Owner)
    {
        if (Owner.GetComponent<SpaceShip>()&&!isDamagedYet)
        {
           isDamagedYet = true;
           return Damage();
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SpaceObject>())
        {
            other.GetComponent<SpaceObject>().Damage(this);
            Damage(other.GetComponent<SpaceObject>());
        }
    }

    void OnDestroy() {
        ObjectDestroyed(this);
        objectCount = Mathf.Clamp(--objectCount, 0, maxObjectCount);
    }
}
