using System;
using UnityEngine;

public class IsBossAttacking : BTCondition
{
   private BossNormalAttackAction action;
    public IsBossAttacking(BossNormalAttackAction action) : base(() => action.CurrentState == BTNodeState.Running)
    {
        this.action = action;
    }
}
