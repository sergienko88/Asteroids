using UnityEngine;
using System.Collections;

public class GUIFireInfo : MonoBehaviour {
    float duration = 3;
    float timer = 0;

	// Use this for initialization
	void Start () {
        
        SpaceShip.AppearShip += (ship) => {
            gameObject.SetActive(true);
        };
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer>duration) {
            gameObject.SetActive(false);
        }
	}

    void OnEnable() {
        timer = 0;
    }
}
