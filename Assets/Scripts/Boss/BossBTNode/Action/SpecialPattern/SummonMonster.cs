using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMonster : BTAction<SummonMonster>
{
    public SummonMonster(Func<SummonMonster, BTNodeState> action) : base(action)
    {
    }
}
