using System;
using UnityEngine;

public class IsBossCastingSpell : BTCondition
{
    private BossMagicAttackAction action;

    public IsBossCastingSpell(BossMagicAttackAction action) : base(() => action.CurrentState == BTNodeState.Running)
    {
        this.action = action;
    }
}
