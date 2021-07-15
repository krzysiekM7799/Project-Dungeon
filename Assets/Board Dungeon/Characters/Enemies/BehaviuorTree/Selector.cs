using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : BTNode
{

	// węzły potomne (childy)
	protected List<BTNode> m_nodes = new List<BTNode>();

	// Konstruktor, który od razu uzupełnia listę
	public Selector(List<BTNode> nodes)
	{
		m_nodes = nodes;
	}

	public override NodeStates Evaluate()
	{
		foreach (BTNode node in m_nodes)
		{
			switch (node.Evaluate())
			{
				case NodeStates.FAILURE:
					continue;
				case NodeStates.SUCCESS:
					m_nodeState = NodeStates.SUCCESS;
					return m_nodeState;
				case NodeStates.RUNNING:
					m_nodeState = NodeStates.RUNNING;
					return m_nodeState;
				default:
					continue;
			}
		}

		m_nodeState = NodeStates.FAILURE;
		return m_nodeState;
	}
}