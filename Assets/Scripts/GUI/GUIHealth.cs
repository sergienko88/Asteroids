using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIHealth : MonoBehaviour {
    Text GUIText;
    // Use this for initialization
    void Start()
    {
        GUIText = GetComponent<Text>();
        GameManager.UpdateHealth += (heath) => { GUIText.text ="Health : " + heath.ToString(); };
    }
}
