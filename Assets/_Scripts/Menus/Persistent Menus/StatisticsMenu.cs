using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsMenu : BaseMenu
{
    private void Awake()
    {
        state = MenuController.MenuStates.Statistics;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Statistics menu opened");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Statistics menu closed");
    }
}