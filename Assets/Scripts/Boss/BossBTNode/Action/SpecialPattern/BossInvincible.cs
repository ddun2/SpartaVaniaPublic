using System;
using UnityEngine;

public class BossInvincible : BTAction<BossInvincible>
{
    public BossInvincible(Func<BossInvincible, BTNodeState> action) : base(action)
    {
    }
}
