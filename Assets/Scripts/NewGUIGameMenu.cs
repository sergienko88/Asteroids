using UnityEngine;
using System.Collections;

public class NewGUIGameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GameManager.PauseAction += (state) => {
        //    gameObject.SetActive(state);
        //};
        gameObject.SetActive(false);
    }
}
