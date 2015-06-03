using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Generator : MonoBehaviour {
    SpaceObject[] ObjectPrafabs = new SpaceObject[0];    
    System.Action<System.Type,int>Spawn;
    Vector3 centerPosition = new Vector3();
    public bool isTest = false;
	// Use this for initialization
	void Start () {
        Spawn += SpawnObject;        
        SpaceObject.ObjectDestroyed += Spawn;
        ObjectPrafabs = Resources.LoadAll<SpaceObject>("Prefabs");
        if (!isTest)
        {
            Create(typeof(Asteroid), Random.Range(10, 15));
            StartCoroutine(Waiter(Random.Range(10f, 60f), null, () =>
            {
                Create(typeof(UFO), UnityEngine.Random.Range(1, 3));
            }));
        }
	}

    void SpawnObject(System.Type type,int val=0)
    {
        if (type == typeof(Asteroid))
        {
            float rnd = Random.value;
            if ((rnd < .3f && (float)Asteroid.objectCount <Asteroid.maxObjectCount*.6f)|| (Asteroid.objectCount <Asteroid.maxObjectCount*.3f) )
            {
                Create(type,(int) UnityEngine.Random.Range(1, (int)Asteroid.maxObjectCount*.5f));
            }
        }
        if (type == typeof(UFO))
        {
            StartCoroutine(Waiter(Random.Range(10f, 20f), null, () =>
            {
                Create(type, UnityEngine.Random.Range(1,3));
            }));
        }
    }

    void Create(System.Type type,int quantity = 1)
    {
        Create(type.ToString(), quantity);
    }

    void Create(string type,int quantity = 1)
    {
      int spawnIndex = System.Array.FindIndex(ObjectPrafabs, t => t.GetType().ToString() == type);
      
      if (spawnIndex != -1)
      {         
              GameObject spawnObject;
              for (int i = 0; i < quantity; i++)
              {
                  if (ObjectPrafabs[spawnIndex].ObjectCount < ObjectPrafabs[spawnIndex].MaxObjectCount)
                  {
                      spawnObject = (GameObject)Instantiate(ObjectPrafabs[spawnIndex].gameObject);
                      Vector2 randomPointOnCircle = Random.insideUnitCircle * 3f;
                      randomPointOnCircle.Normalize();
                      randomPointOnCircle *= Random.Range(1f, 10f);
                      Vector3 newPosition = new Vector3(randomPointOnCircle.x, randomPointOnCircle.y, centerPosition.z);
                      spawnObject.transform.position += newPosition;
                      ObjectPrafabs[spawnIndex].ObjectCount = Mathf.Clamp(++ObjectPrafabs[spawnIndex].ObjectCount, 0, ObjectPrafabs[spawnIndex].MaxObjectCount);                  
                  }
              }
      }
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {            
            Create(typeof(Asteroid),5);
            //Create(typeof(UFO), 2);
        }
    }

    IEnumerator Waiter(float duration, System.Action do_before, System.Action do_after)
    {
        do_before += () => { };
        do_after += () => { };
        do_before();
        yield return new WaitForSeconds(duration);
        do_after();
    }
}
