using UnityEngine;
using System.Collections;

public class OptionsButton : ElementGUI {
    public static System.Action <bool,GameObject> ShowOptions;
    protected override void Start()
    {
        base.Start();
        ShowOptions += (state,parent) => { };
        PressAction += () =>
        {
            ShowOptions(true,transform.parent.gameObject);
        };
    }
}
