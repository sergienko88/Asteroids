using UnityEngine;
using System.Collections;

public class DefaultGuiInitializator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        System.Array.ForEach(transform.GetComponentsInChildren<Transform>(true), t => t.gameObject.SetActive(true));
	}

}
