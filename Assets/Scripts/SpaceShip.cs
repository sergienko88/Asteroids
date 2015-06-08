using UnityEngine;
using System.Collections;

public class SpaceShip : SpaceObject {

    static int objectCount = 0;
    static int maxObjectCount = 1;
    Vector3 start_position = Vector3.zero;
    
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

	// Use this for initialization
	protected override void Start () {
        start_position = transform.position;
        isDamagedYet = false;
        GameManager.GamePlay += (state) =>
        {
            gameObject.SetActive(state);
        };
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
        GetComponent<Collider2D>().enabled = false;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        MonoBehaviour[] allmonos = GetComponentsInChildren<MonoBehaviour>();
        System.Array.ForEach(allmonos, am => am.enabled = false);
        System.Array.ForEach(renderers, r => r.enabled = false);
        yield return new WaitForSeconds(2f);
        Collider2D[] objs = Physics2D.OverlapCircleAll((Vector2)transform.position,2f);
        for (int i = 0; i < objs.Length; i++)
        {            
            Destroy(objs[i].gameObject);
        }
        yield return new WaitForEndOfFrame();
        GetComponent<Collider2D>().enabled = true;
        System.Array.ForEach(allmonos, am => am.enabled = true);
        System.Array.ForEach(renderers, r => r.enabled = true);
    }
}
