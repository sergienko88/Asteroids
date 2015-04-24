using UnityEngine;
using System.Collections;

public class SpawnEntity : MonoBehaviour
{
    protected Transform centerPosition;
    protected int maxspawn_objects = 10;
    protected int current_spawn_objects = 0;
    // Use this for initialization
	protected void Start () {
        
	}

    float timer = 0;
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            timer = 0;
            CreateObject();
        }
	
	}


    void CreateObject()
    {
        //if (spawnObjs.Length == 0) return;
        GameObject obj = (GameObject)Instantiate(gameObject);
        var randomPointOnCircle = Random.insideUnitCircle*3f;
        randomPointOnCircle.Normalize();
        randomPointOnCircle *= Random.Range(1f, 10f);
        var newPosition = new Vector3(randomPointOnCircle.x,randomPointOnCircle.y,centerPosition.transform.position.z);
        obj.transform.position += newPosition;
    }
}
