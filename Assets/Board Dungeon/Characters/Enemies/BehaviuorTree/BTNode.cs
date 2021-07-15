using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class BTNode
{

	// Obecny stan wierzchołka
	protected NodeStates m_nodeState;

	// Geter pozwalający pobrać aktualny stan wierzchołka
	public NodeStates nodeState
	{
		get { return m_nodeState; }
	}

	// Pusty konstruktor
	public BTNode() { }

	// Abstrakcyjna metoda wyliczająca wartość wierzchołka
	public abstract NodeStates Evaluate();
}