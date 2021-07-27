using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDistanceCondition : BTNode
{
    [SerializeField] float distanceLessThanToPlayer;
    [SerializeField] float distanceGreaterThanToPlayer;
    private Transform playerTransform;
    private Transform enemyTransform;
    private float distanceToPlayer;
    private float sumOfTheCharacterRadius;
    
    public void SetEnemyDistanceConditionProperties(Transform playerTransform, Transform enemyTransform, float playerRadius, float enemyRadius)
    {
        this.playerTransform = playerTransform;
        this.enemyTransform = enemyTransform;
        sumOfTheCharacterRadius = playerRadius + enemyRadius;       
    }

    private void GetDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(playerTransform.position, enemyTransform.position);
        //distance between capsule colliders, not middle of character
        distanceToPlayer -= sumOfTheCharacterRadius;
    }

    //Main evaluate method, logic of node
    public override NodeStates Evaluate()
    {
        GetDistanceToPlayer();
        if(distanceLessThanToPlayer != 0)
        {
            if (!(distanceToPlayer < distanceLessThanToPlayer))
            {
                return NodeStates.FAILURE;
            }
        }
        if(!(distanceToPlayer > distanceGreaterThanToPlayer))
        {
            return NodeStates.FAILURE;
        }

        return NodeStates.SUCCESS;
    }
 
}
