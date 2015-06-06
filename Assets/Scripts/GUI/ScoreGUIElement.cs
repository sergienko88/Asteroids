using UnityEngine;
using System.Collections;

public class ScoreGUIElement : ElementGUI {

    protected override void Start()
    {
        base.Start();
        GameManager.UpdateScore += (score) =>
        {
            elementTextMesh.text = "Score : " + score.ToString(); 
        };
    }
    protected override void Update()
    {
        
    }
}
