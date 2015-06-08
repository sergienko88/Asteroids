using UnityEngine;
using System.Collections;

public class BackButton : ElementGUI
{
    MenuGUI menu;

    protected override void Start()
    {
        base.Start();
        menu = transform.parent.GetComponent<MenuGUI>();
        PressAction += () =>
        {
            if (menu)
            {
                if(menu.lastMenu) menu.lastMenu.SetActive(true);
                menu.gameObject.SetActive(false);
            }
            else
            {
                if (transform.parent != null)
                {
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        };
    }
}
