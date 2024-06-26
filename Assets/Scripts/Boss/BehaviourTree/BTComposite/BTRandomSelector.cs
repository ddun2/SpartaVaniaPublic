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

        // �ε����� ť�� �߰��ϰ�, ť�� ũ�Ⱑ 3�� �ʰ��ϸ� ���� ������ �ε����� ����
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
