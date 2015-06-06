using UnityEngine;
using System.Collections;

public class StartGameButton : ElementGUI {

    protected override void Start()
    {
        base.Start();
        PressAction += () => {
            GameManager.GamePlay(true);    
        };
    }
}
