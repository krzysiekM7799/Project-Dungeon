using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHpNode : BTNode
{
    [SerializeField] private float hpPercentToRunAway;
    EnemyStats enemyStats;
    

    public CheckHpNode(int hpPercentToRunAway, EnemyStats enemyStats)
    {
        this.hpPercentToRunAway = (float)hpPercentToRunAway/100;
        this.enemyStats = enemyStats;
    }

    public override NodeStates Evaluate()
    {
        if (enemyStats.Hp <= enemyStats.MaxHp * hpPercentToRunAway)
        {
            Debug.Log("Succes hp");
            return NodeStates.SUCCESS;

        }
            
        return NodeStates.FAILURE;
    }
}
