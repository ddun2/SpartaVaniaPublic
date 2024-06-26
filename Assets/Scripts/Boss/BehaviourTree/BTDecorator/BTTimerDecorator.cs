using UnityEngine;

public class BTTimerDecorator : BTNode
{
    private BTNode child;
    private float duration;
    private float startTime;

    public BTTimerDecorator(BTNode child, float duration)
    {
        this.child = child;
        this.duration = duration;
    }

    public override BTNodeState Evaluate()
    {
        if (Time.time - startTime < duration)
        {
            return BTNodeState.Running;
        }
        else
        {
            startTime = Time.time;
            return child.Evaluate();
        }
    }
}
