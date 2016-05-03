using UnityEngine;
using System.Collections;

public class GUIHealth : MonoBehaviour {
    UnityEngine.UI.Text TextGUI;
    // Use this for initialization
    void Start () {
        TextGUI = GetComponent<UnityEngine.UI.Text>();
        TextGUI.text = GameManager.Health.ToString();
        GameManager.UpdateHealth += (val) => {
            TextGUI.text = val.ToString();
        };
	}
}
