using UnityEngine;
using System.Collections.Generic;

public class BTRandomSelector : BTNode
{
    private BTNode currentNode = null;
    private Queue<int> recentIndices = new Queue<int>();

    public override BTNodeState Evaluate()
    {
        if (currentNode != null && currentNode.Evaluate() == BTNodeState.Running)
        {
            return BTNodeState.Running;
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, children.Count);
        } while (recentIndices.Contains(randomIndex));

        // 인덱스를 큐에 추가하고, 큐의 크기가 3을 초과하면 가장 오래된 인덱스를 제거
        recentIndices.Enqueue(randomIndex);
        if (recentIndices.Count > 3)
        {
            recentIndices.Dequeue();
        }

        currentNode = children[randomIndex];
        BTNodeState nodeState = currentNode.Evaluate();

        if (nodeState != BTNodeState.Running)
        {
            currentNode = null;
        }

        return nodeState;
    }
}
