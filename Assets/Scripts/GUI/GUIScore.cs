using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScore : MonoBehaviour {
    Text GUIText;
	// Use this for initialization
	void Start () {
        GUIText = GetComponent<Text>();
        GameManager.UpdateScore += (score) => { GUIText.text = "Score : "+score.ToString(); };
	}
}
