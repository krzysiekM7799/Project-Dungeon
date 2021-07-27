
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAngleCondition : BTNode
{
    [SerializeField] float angleLessThanToPlayer;
    [SerializeField] float angleGreaterThanToPlayer;
    private Transform playerTransform;
    private Transform enemyTransform;
    private float angleToPlayer;


    public void SetEnemyAngleConditionProperties(Transform playerTransform, Transform enemyTransform)
    {
        this.playerTransform = playerTransform;
        this.enemyTransform = enemyTransform;
    }

    private void GetAngleToPlayer()
    {
        angleToPlayer = ThingCalculator.FindAngle(enemyTransform.forward, playerTransform.position - enemyTransform.position, enemyTransform.up);     
        angleToPlayer = System.Math.Abs(angleToPlayer);
    }
    
    //Main evaluate method, node logic
    public override NodeStates Evaluate()
    {
        GetAngleToPlayer();
        if (angleLessThanToPlayer != 0)
        {
            if (!(angleToPlayer < angleLessThanToPlayer))
            {
                return NodeStates.FAILURE;
            }
        }
        if (!(angleToPlayer > angleGreaterThanToPlayer))
        {
            return NodeStates.FAILURE;
        }

        return NodeStates.SUCCESS;
    }

}
