using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAbilityConditions
{
    Sequence conditionsEnemyAbility;
    [EnumNamedArray(typeof(AbilityConditionType))]
    [SerializeField] protected bool[] availableConditions = new bool[System.Enum.GetNames(typeof(AbilityConditionType)).Length];
    [SerializeField] EnemyDistanceCondition enemyDistanceCondition = new EnemyDistanceCondition();
    [SerializeField] EnemyAngleCondition enemyAngleCondition = new EnemyAngleCondition();
    private EnemyCharacter enemyCharacter;
    public void SetEnemyAbilityConditionsProperties(EnemyCharacter enemyCharacter)
    {
        this.enemyCharacter = enemyCharacter;
    }
    /*float distanceLessThanToPlayer;
    float distanceGreaterThanToPlayer;
    float angleLessThanToPlayer;
    float angleGreaterThanToPlayer;
    float playerSpeedLessThan;
    float playerSpeedGreaterThan;
    bool playerCantUseAbilities;
    bool playerCantUseBasicAttacks;*/
    public Sequence MakeEnemyAbilityConditionsNode()
    {
        List<BTNode> conditionsEnemyAbilityChildren = new List<BTNode>();

        if (availableConditions[(int)AbilityConditionType.DistanceCondition])
        {
            enemyDistanceCondition.SetEnemyDistanceConditionProperties(GameController.instance.PlayerTransform, enemyCharacter.transform, GameController.instance.PlayerCharacter.GetRealRadiusRadius(), enemyCharacter.GetRealRadiusRadius());
            conditionsEnemyAbilityChildren.Add(enemyDistanceCondition);
        }
        if (availableConditions[(int)AbilityConditionType.AngleCondition])
        {
            enemyAngleCondition.SetEnemyAngleConditionProperties(GameController.instance.PlayerTransform, enemyCharacter.transform, GameController.instance.PlayerCharacter.GetRealRadiusRadius(), enemyCharacter.GetRealRadiusRadius());
            conditionsEnemyAbilityChildren.Add(enemyAngleCondition);
        }

        return conditionsEnemyAbility = new Sequence(conditionsEnemyAbilityChildren);
       
    }

   
}
