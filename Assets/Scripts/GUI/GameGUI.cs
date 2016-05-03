using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.ChangeGameStatus += (state) => {
            gameObject.SetActive(state == GameState.Play);
        };
        gameObject.SetActive(false);

    }
}
