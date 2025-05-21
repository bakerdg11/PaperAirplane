using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : BaseMenu
{
    private void Awake()
    {
        state = MenuController.MenuStates.Settings;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Settings menu opened");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Settings menu closed");
    }
}
