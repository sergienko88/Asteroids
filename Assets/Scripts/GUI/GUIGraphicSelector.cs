using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GUIGraphicSelector : ElementGUI {    
    public GraphicType type = GraphicType.Mesh;
    TextMesh textMesh;
    Color ONColor = Color.cyan;
    Color OFFColor = Color.white;
	// Use this for initialization
    protected override void Start()
    {
        base.Start();        
        textMesh = GetComponent<TextMesh>();
        textMesh.color = type == GameManager.GraphicType ? ONColor : OFFColor;
        GameManager.ChangeGraphicEvent += (graphicType) => {
            textMesh.color = type == graphicType ? ONColor : OFFColor;
        };
        PressAction += () => {
            if (GameManager.GraphicType != type)
            {
                GameManager.ChangeGraphic(type);             
            }
        };
    }

}
