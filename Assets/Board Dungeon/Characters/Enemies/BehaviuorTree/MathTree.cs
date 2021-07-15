using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MathTree : MonoBehaviour
{

    public Color m_evaluating;
    public Color m_succeeded;
    public Color m_failed;

    public Selector m_rootNode;

    public ActionNode m_node2A;
    public Inverter m_node2B;
    public ActionNode m_node2C;
    public ActionNode m_node3;

    public GameObject m_rootNodeBox;
    public GameObject m_node2aBox;
    public GameObject m_node2bBox;
    public GameObject m_node2cBox;
    public GameObject m_node3Box;

    public int m_targetValue = 20;
    private int m_currentValue = 0;

    [SerializeField]
    private Text m_valueLabel;

    void Start()
    {
        // Zaczynamy od najniższej warstwie, w niej mamy tylko node 3
        m_node3 = new ActionNode(NotEqualToTarget);

        // Kolejna warstwa, czyli węzły z poziomu 2
        m_node2A = new ActionNode(AddTen);

        // Węzłem 2B jest dekoratorem, dlatego jako parametr podajemy node 3, który jest jego potomkiem
        m_node2B = new Inverter(m_node3);

        m_node2C = new ActionNode(AddTen);

        // Przygotowujemy listę potomków dla korzenia (selector przyjmuje listę węzłów!)
        List<BTNode> rootChildren = new List<BTNode>();
        rootChildren.Add(m_node2A);
        rootChildren.Add(m_node2B);
        rootChildren.Add(m_node2C);

        // Tworzymy korzeń, podając mu listę potomków
        m_rootNode = new Selector(rootChildren);
        m_valueLabel.text = m_currentValue.ToString();
        m_rootNode.Evaluate();

        UpdateBoxes();
    }

    private void Update()
    {
       

    }


    public void Licz(){

        m_rootNode.Evaluate();
        m_valueLabel.text = m_currentValue.ToString();
        UpdateBoxes();

    }

    private void UpdateBoxes()
    {
        if (m_rootNode.nodeState == NodeStates.SUCCESS)
        {
            SetSucceeded(m_rootNodeBox);
        }
        else if (m_rootNode.nodeState == NodeStates.FAILURE)
        {
            SetFailed(m_rootNodeBox);
        }

        if (m_node2A.nodeState == NodeStates.SUCCESS)
        {
            SetSucceeded(m_node2aBox);
        }
        else if (m_node2A.nodeState == NodeStates.FAILURE)
        {
            SetFailed(m_node2aBox);
        }

        if (m_node2B.nodeState == NodeStates.SUCCESS)
        {
            SetSucceeded(m_node2bBox);
        }
        else if (m_node2B.nodeState == NodeStates.FAILURE)
        {
            SetFailed(m_node2bBox);
        }

        if (m_node2C.nodeState == NodeStates.SUCCESS)
        {
            SetSucceeded(m_node2cBox);
        }
        else if (m_node2C.nodeState == NodeStates.FAILURE)
        {
            SetFailed(m_node2cBox);
        }

        if (m_node3.nodeState == NodeStates.SUCCESS)
        {
            SetSucceeded(m_node3Box);
        }
        else if (m_node3.nodeState == NodeStates.FAILURE)
        {
            SetFailed(m_node3Box);
        }
    }


    private void SetSucceeded(GameObject box)
    {
        box.GetComponent<Renderer>().material.color = m_succeeded;
    }

    private void SetFailed(GameObject box)
    {
        box.GetComponent<Renderer>().material.color = m_failed;
    }


    private NodeStates AddTen()
    {
        m_currentValue += 10;
        m_valueLabel.text = m_currentValue.ToString();
        if (m_currentValue == m_targetValue)
        {
            return NodeStates.SUCCESS;
        }
        else
        {
            return NodeStates.FAILURE;
        }
    }

    private NodeStates NotEqualToTarget()
    {
        if (m_currentValue != m_targetValue)
        {
            return NodeStates.SUCCESS;
        }
        else
        {
            return NodeStates.FAILURE;
        }
    }

}