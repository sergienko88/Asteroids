using UnityEngine;
using System.Collections;

public class GUIScore : MonoBehaviour {
    UnityEngine.UI.Text TextGUI;
    // Use this for initialization
    void Start()
    {
        TextGUI = GetComponent<UnityEngine.UI.Text>();
        GameManager.UpdateScore += (val) => {
            TextGUI.text = val.ToString();
        };
    }
}
