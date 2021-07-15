using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sequence : BTNode
{

    // Lista węzłów potomnych
    private List<BTNode> m_nodes = new List<BTNode>();

    // Konstruktor, pozwalający ustawić listę childów
    public Sequence(List<BTNode> nodes)
    {
        m_nodes = nodes;
    }
    public Sequence()
    {
       
    }
    public void SetChildrenOfNode(List<BTNode> nodes)
    {
        m_nodes = nodes;
    }
    public List<BTNode> MakeSimpleSequenceNode(ActionNode.ActionNodeDelegate CheckConditions, ActionNode.ActionNodeDelegate Trigge)
    {
        var conditions = new ActionNode(CheckConditions);

        var trigger = new ActionNode(Trigge);
        List<BTNode> simpleNodeChildren = new List<BTNode>();
        simpleNodeChildren.Add(conditions);
        simpleNodeChildren.Add(trigger);
        return simpleNodeChildren;

    }

    public override NodeStates Evaluate()
    {
        bool anyChildRunning = false;

        foreach (BTNode node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;
                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
            }
        }

        m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
        return m_nodeState;
    }
}