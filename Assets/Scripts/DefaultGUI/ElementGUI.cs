using UnityEngine;
using System.Collections;

public class ElementGUI : MonoBehaviour {
    protected TextMesh elementTextMesh;
    protected Camera GUICamera;
    RaycastHit hit;
    protected System.Action PressAction;
	// Use this for initialization
    virtual protected void Start()
    {
        elementTextMesh = GetComponent<TextMesh>();
        GUICamera = transform.root.GetComponent<Camera>();
        PressAction += () => { };        
	}
    virtual protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<Collider>().Raycast(GUICamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
                PressAction();
            }
        }
    }
}
