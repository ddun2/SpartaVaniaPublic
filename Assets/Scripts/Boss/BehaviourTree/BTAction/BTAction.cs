public class BTAction<T> : BTNode where T : BTAction<T>
{
    private System.Func<T, BTNodeState> action;

    public BTAction(System.Func<T, BTNodeState> action)
    {
        this.action = action;
    }

    public override BTNodeState Evaluate()
    {
        return action((T)this);
    }
}

