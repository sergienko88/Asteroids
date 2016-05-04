using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Generator : MonoBehaviour {
    SpaceObject[] ObjectPrafabs = new SpaceObject[0];    
    System.Action<System.Type>Spawn;
    Vector3 centerPosition = new Vector3();
    public bool isTest = false;
	// Use this for initialization
	void Start () {
        Spawn += SpawnObject;//подписка на событие создания новых объектов         
        SpaceObject.ObjectDestroyed += SpaceObjectTypeDetect;// подписка на событие уничтожения объекта
        ObjectPrafabs = Resources.LoadAll<SpaceObject>("Prefabs");
        if (!isTest)
        {
            Create(typeof(Asteroid), Random.Range(10, 15));
            StartCoroutine(Waiter(Random.Range(10f, 60f), null, () =>
            {
                Create(typeof(UFO), UnityEngine.Random.Range(1, 3));
            }));
        }

        //При старте убираем все объекты и создаем новые
        GameManager.ChangeGameStatus += (state) => { 
            if(state == GameState.Play) {
                if (!isTest)
                {
                    System.Array.ForEach(GameObject.FindObjectsOfType<SpaceObject>(), so =>
                    {
                        if (!(so is SpaceShip))
                        {
                            Destroy(so.gameObject);
                        }
                    });
                    Create(typeof(Asteroid), Random.Range(10, 15));
                    StartCoroutine(Waiter(Random.Range(10f, 60f), null, () =>
                    {
                        Create(typeof(UFO), UnityEngine.Random.Range(1, 3));
                    }));
                }
            }
        };
	}

    void SpaceObjectTypeDetect(SpaceObject obj) 
    {
        if (obj != null)
        {
            SpawnObject(obj.GetType());
        }
    }

    void SpawnObject(System.Type type)
    {
        if (type == typeof(Asteroid))
        {
            float rnd = Random.value;
            if ((rnd < .3f && (float)Asteroid.objectCount <Asteroid.maxObjectCount*.5f))
            {
                Create(type,(int) UnityEngine.Random.Range(1,(int)(Asteroid.maxObjectCount- Asteroid.objectCount)*.5f));
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
                spawnObject.transform.position = centerPosition + newPosition;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player)
                {
                    while (Vector3.Distance(spawnObject.transform.position, player.transform.position) < 5f)
                    {
                        randomPointOnCircle = Random.insideUnitCircle * 3f;
                        randomPointOnCircle.Normalize();
                        randomPointOnCircle *= Random.Range(1f, 10f);
                        newPosition = new Vector3(randomPointOnCircle.x, randomPointOnCircle.y, centerPosition.z);
                        spawnObject.transform.position = centerPosition + newPosition;
                    }
                }
                ObjectPrafabs[spawnIndex].ObjectCount = Mathf.Clamp(++ObjectPrafabs[spawnIndex].ObjectCount, 0, ObjectPrafabs[spawnIndex].MaxObjectCount);                  
            }
        }
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
