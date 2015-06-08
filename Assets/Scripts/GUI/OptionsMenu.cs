using UnityEngine;
using System.Collections;

public class OptionsMenu : MenuGUI {
   
	// Use this for initialization
	void Start () {
        OptionsButton.ShowOptions += (state,parent) => {
            lastMenu = parent;
            parent.SetActive(!state); 
            gameObject.SetActive(state);
        };

        GameManager.PauseAction += (state) =>
        {
            if (!state)
            {
                gameObject.SetActive(false);
            }
        };
        gameObject.SetActive(false);
	}
}
