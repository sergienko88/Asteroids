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
        isDamagedYet = false;
	}

    public override bool Damage()
    {
        transform.position = start_position;
        StartCoroutine(Wait());
        ObjectDestroyed(GetType(),0);
        objectCount = Mathf.Clamp(--objectCount, 0, maxObjectCount);
        return true;
    }

    public override bool Damage(Transform Owner)
    {     
        if (Owner != transform)
        {
            isDamagedYet = true;
            return Damage();
        }
        return false;
    }

    IEnumerator Wait()
    {
        collider2D.enabled = false;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        MonoBehaviour[] allmonos = GetComponentsInChildren<MonoBehaviour>();
        System.Array.ForEach(allmonos, am => am.enabled = false);
        System.Array.ForEach(renderers, r => r.enabled = false);
        yield return new WaitForSeconds(2f);
        collider2D.enabled = true;
        renderer.enabled = true;
        System.Array.ForEach(allmonos, am => am.enabled = true);
        System.Array.ForEach(renderers, r => r.enabled = true);
    }
}
