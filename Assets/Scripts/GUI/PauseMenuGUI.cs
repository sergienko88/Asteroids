using UnityEngine;
using System.Collections;

public class PauseMenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.ChangeGameStatus += (state) => {
            gameObject.SetActive(state == GameState.Pause);
        };
        gameObject.SetActive(false);
	}
}
