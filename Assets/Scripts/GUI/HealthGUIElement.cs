using UnityEngine;
using System.Collections;

public class HealthGUIElement : ElementGUI {

    protected override void Start()
    {
        base.Start();
        GameManager.UpdateHealth += (health) =>
        {
            elementTextMesh.text = "Health : " + health.ToString();
        };
    }

    protected override void Update()
    {
        
    }
}
