using UnityEngine;
using System.Collections;

public class MainMenu : MenuGUI {

	// Use this for initialization
	void Start () {
        GameManager.ChangeGameStatus += (status) => {
            gameObject.SetActive(status == GameState.MainMenu);
        };
	}

}
