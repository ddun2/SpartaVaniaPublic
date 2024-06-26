using System;

public class SpecialAttack : BTAction<SpecialAttack>
{
    public SpecialAttack(Func<SpecialAttack, BTNodeState> action) : base(action)
    {
    }
}
