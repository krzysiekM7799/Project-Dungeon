using UnityEngine;
using System.Collections;

public class Inverter : BTNode
{

    // Potomny węzeł
    private BTNode m_node;

    // Funkcja zwracająca potomny wierzchołek
    public BTNode node
    {
        get { return m_node; }
    }

    // Konstruktor ustawiający potomny wierzchołek
    public Inverter(BTNode node)
    {
        m_node = node;
    }

    public override NodeStates Evaluate()
    {
        switch (m_node.Evaluate())
        {
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
        }

        m_nodeState = NodeStates.SUCCESS;
        return m_nodeState;
    }
}