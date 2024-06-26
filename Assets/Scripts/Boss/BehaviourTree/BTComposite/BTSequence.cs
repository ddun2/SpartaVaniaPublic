using System.Collections.Generic;

public class BTSequence : BTNode
{
    public BTSequence(List<BTNode> children)
    {
        this.children = children;
    }

    public override BTNodeState Evaluate()
    {
        bool isAnyChildRunning = false;

        foreach (BTNode node in children)
        {
            BTNodeState result = node.Evaluate();
            if (result == BTNodeState.Failure)
            {
                return BTNodeState.Failure;
            }
            else if (result == BTNodeState.Running)
            {
                isAnyChildRunning = true;
            }
        }

        return isAnyChildRunning ? BTNodeState.Running : BTNodeState.Success;
    }
}

