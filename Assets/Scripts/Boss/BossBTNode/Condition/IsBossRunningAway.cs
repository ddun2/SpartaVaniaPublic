using System;
using UnityEngine;

public class IsBossRunningAway : BTCondition
{
    private BossRunawayAction action;

    public IsBossRunningAway(BossRunawayAction action) : base(() => action.CurrentState == BTNodeState.Running)
    {
        this.action = action;
    }
}
