using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsMenu : BaseMenu
{
    private void Awake()
    {
        state = MenuController.MenuStates.Records;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Records menu opened");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Records menu closed");
    }
}