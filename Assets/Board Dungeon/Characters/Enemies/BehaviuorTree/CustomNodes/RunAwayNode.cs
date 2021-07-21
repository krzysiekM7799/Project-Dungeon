using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayNode
{
    int hpPercentToRunAway;
    EnemyStats enemyStats;

    public RunAwayNode(int hpPercentToRunAway, EnemyStats enemyStats)
    {
        this.hpPercentToRunAway = hpPercentToRunAway;
        this.enemyStats = enemyStats;
    }

    private  BTNode MakeRunAwayNode(List<BTNode> rootNodeChildren, BTNode conditionNode = null)
    {
        Sequence runAwayNode = new Sequence();
        List<BTNode> runAwayChildren = new List<BTNode>();

        if (hpPercentToRunAway != 0)
            runAwayChildren.Add(new CheckHpNode(hpPercentToRunAway, enemyStats));
        if(conditionNode != null)
        {
            runAwayChildren.Add(conditionNode);
        }
        runAwayChildren.Add(new ActionNode(TriggeRunAway));
        runAwayNode.SetChildrenOfNode(runAwayChildren);
       

        return runAwayNode;

    }

    private NodeStates TriggeRunAway()
    {
        return NodeStates.SUCCESS;
    }

 
}
