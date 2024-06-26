public class BTInverter : BTNode
{
    private BTNode child;

    public BTInverter(BTNode child)
    {
        this.child = child;
    }

    public override BTNodeState Evaluate()
    {
        switch (child.Evaluate())
        {
            case BTNodeState.Failure:
                return BTNodeState.Success;
            case BTNodeState.Success:
                return BTNodeState.Failure;
            default:
                return BTNodeState.Running;
        }
    }
}
