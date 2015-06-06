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
        gameObject.SetActive(false);
	}
}
