using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Generator : MonoBehaviour {
    SpaceObject[] ObjectPrafabs = new SpaceObject[0];    
    System.Action<System.Type>Spawn;
    Vector3 centerPosition = new Vector3();
	// Use this for initialization
	void Start () {
        Spawn += SpawnObject;        
        SpaceObject.ObjectDestroyed += Spawn;
        ObjectPrafabs = Resources.LoadAll<SpaceObject>("Prefabs");
	}

    void SpawnObject(System.Type type)
    {
        if (type == typeof(Asteroid))
        {
            Create(type,UnityEngine.Random.Range(1,Asteroid.maxObjectCount));
        }
        if (type == typeof(UFO))
        {

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
            Create(typeof(UFO), 2);
        }
    }
}
