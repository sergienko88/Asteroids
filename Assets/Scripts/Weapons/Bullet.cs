using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    float speed = 4f;
    Vector3 start_position = Vector3.zero;
    float max_distance = 5f;
    float current_distance = 0f;
    bool isMoved = true;
    SpaceObject owner = null;
	// Use this for initialization

    void Awake()
    {
        Destroy(gameObject.GetComponent<SphereCollider>());
    }

	void Start () {        
        start_position = transform.position;
        isMoved = transform.name != "Bullet_prefab";
        if (isMoved) gameObject.AddComponent<SpaceEndlessWrapper>();
        StartCoroutine(WaitEndFrameAndDo(() =>
                {
                    Destroy(gameObject.GetComponent<SphereCollider>());
                },
                 () =>
                 {
                     if (!gameObject.GetComponent<CircleCollider2D>())
                     {
                         gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
                     }
                     if (!gameObject.GetComponent<Rigidbody2D>())
                     {
                         gameObject.AddComponent<Rigidbody2D>();
                         gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                         gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                     }
                 }
             ));
 	}
	
	// Update is called once per frame
	void Update () {
        if (!isMoved) return;
        transform.position += transform.up * speed * Time.deltaTime;
        current_distance += speed * Time.deltaTime;       
        if (current_distance >= max_distance)
        {
            Destroy(gameObject);
        }        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isMoved) return;
        if (other.GetComponent<SpaceObject>() != owner)
        {            
            if (other.GetComponent<SpaceObject>())
            {                
                if (other.GetComponent<SpaceObject>().Damage(owner))
                {                    
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Initialize(SpaceObject Owner){
        owner = Owner;
    }
     
    IEnumerator WaitEndFrameAndDo(System.Action action_before_end_of_frame, System.Action action_after_end_of_frame)
    {
        if (action_before_end_of_frame != null)
        {
            action_before_end_of_frame();
        }
        yield return new WaitForEndOfFrame();
        if (action_after_end_of_frame != null)
            action_after_end_of_frame();
    }
}
