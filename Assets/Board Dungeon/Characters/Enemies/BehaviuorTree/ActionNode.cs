using UnityEngine;
using System.Collections;

public class ActionNode : BTNode
{

    // Delegat, który wymusza nam wygląd funkcji dołączonej do akcji
    public delegate NodeStates ActionNodeDelegate();
    public delegate NodeStates ActionNodeDelegateInt(int _int);
    public delegate NodeStates ActionNodeDelegateFloat(float _float);

    // Akcja, która musi mieć składnię delegata (czyli zwracać NodeStates i nie mieć argumentów)

    private ActionNodeDelegate action;
    private ActionNodeDelegateInt actionInt;
    private ActionNodeDelegateFloat actionFloat;
    private int _int;
    private float _float;
    ParametrType parametrType;


    enum ParametrType
    {
        INT,
        FLOAT,
        NULL,
    }

    // Konstruktor, który wymaga od nas podania nazwy funkcji, która będzie wykonywać logikę węzła


    public ActionNode(ActionNodeDelegate action)
    {
        this.action = action;
        parametrType = ParametrType.NULL;

    }

    public ActionNode(ActionNodeDelegateInt action, int _int)
    {
        actionInt = action;
        this._int = _int;
        parametrType = ParametrType.INT;

    }
    public ActionNode(ActionNodeDelegateFloat action, float _float)
    {
        actionFloat = action;
        this._float = _float;
        parametrType = ParametrType.FLOAT;

    }

    public override NodeStates Evaluate()
    {
        NodeStates nodeState = NodeStates.FAILURE;
      
        switch (parametrType)
        {
            case ParametrType.INT:
               nodeState = actionInt.Invoke(_int);
                break;
            case ParametrType.FLOAT:
                nodeState = actionFloat.Invoke(_float);
                break;
            case ParametrType.NULL:
                nodeState = action.Invoke();
                break;
        }


        switch (nodeState)
        {
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
            default:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
        }
    }
}