using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesMenu : BaseMenu
{
    private void Awake()
    {
        state = MenuController.MenuStates.Upgrades;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Upgrades menu opened");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Upgrades menu closed");
    }
}