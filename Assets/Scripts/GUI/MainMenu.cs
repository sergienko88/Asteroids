using UnityEngine;
using System.Collections;

public class MainMenu : MenuGUI {

	// Use this for initialization
	void Start () {
        GameManager.GamePlay += (state) =>
        {
            gameObject.SetActive(!state);
        };
	}

}
