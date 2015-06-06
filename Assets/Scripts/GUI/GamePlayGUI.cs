using UnityEngine;
using System.Collections;

public class GamePlayGUI: MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.GamePlay += (state) => {
            gameObject.SetActive(state);
        };
        gameObject.SetActive(false);
	}
	
}
