﻿using UnityEngine;
using System.Collections;

public class ResumeGameButton : ElementGUI {
    protected override void Start()
    {
        base.Start();
        PressAction += () =>
        {
            GameManager.instance.GameStatus = GameState.Play;
        };
    }
}