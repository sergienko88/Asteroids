using UnityEngine;
using System.Collections;

public class ExitButton : ElementGUI
{

    protected override void Start()
    {
        base.Start();
        PressAction += () => {
            GameManager.instance.QuitGame();
        };
    }
	
}
