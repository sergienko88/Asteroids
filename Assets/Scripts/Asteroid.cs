using UnityEngine;
using System.Collections;

public class Asteroid : SpaceObject
{	
    float speed = 1f;
    int life = 2;
    Vector3 movedir = Vector3.zero;
    bool isFragment = false;

    public static int objectCount = 0;
    public static int maxObjectCount = 20;

    public override int ObjectCount
    {
        get { return Asteroid.objectCount; }
        set { Asteroid.objectCount = value; }
    }


    public override int MaxObjectCount
    {
        get { return Asteroid.maxObjectCount; }
        set { Asteroid.maxObjectCount = value; }
    }

	protected override void Start(){
        base.Start();
        movedir = (Vector3)(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
	}
	// Update is called once per frame
	void Update () {
        transform.Translate(movedir * Time.deltaTime * speed, Space.World);
	}

    public override bool Damage()
    {
		life--;
		if(life>0){
			CreateFragments(UnityEngine.Random.Range(2,4));
		}
        if (!isFragment)
        {
            ObjectDestroyed(GetType());
            objectCount = Mathf.Clamp(--objectCount, 0, maxObjectCount);
        }
		Destroy(gameObject);
        return true;
	}

    public override bool Damage(Transform owner)
    {        
        if (owner.GetComponent<SpaceShip>())
        {           
           return Damage();            
        }
        return false;
    }

	void Initialize(int life,float speed,Vector2 scale,bool isFragment = false){
		this.life = life;
		this.speed = speed;
        this.isFragment = isFragment;
		transform.localScale = scale;
        int directionLeftOrRight = UnityEngine.Random.Range(0, 2) == 1 ? -1 : 1;
        transform.Rotate(new Vector3(0, 0, directionLeftOrRight * UnityEngine.Random.Range(0, 90)));
	}

	void CreateFragments(int quantity){ 

		for (int i=0;i<quantity;i++){
			((GameObject)Instantiate(gameObject,transform.position,transform.rotation)).GetComponent<Asteroid>().Initialize(
				life,
				speed*UnityEngine.Random.Range(1f,1.5f),
				transform.localScale*UnityEngine.Random.Range(.3f,.8f),
                true		    
			);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
        if (other.GetComponent<SpaceObject>())
        {            
            other.GetComponent<SpaceObject>().Damage(transform);
            Damage(other.transform);
        }
     }
}
