using System.Collections.Generic;
using UnityEngine;

//EnemyAbilityConditions is based on the structure of the behavioral tree, so it i easy to extend
[System.Serializable]
public class EnemyAbilityConditions
{
    //An array of the available conditions that can be selected in the inspector
    [EnumNamedArray(typeof(AbilityConditionType))]
    [SerializeField] protected bool[] availableConditions = new bool[System.Enum.GetNames(typeof(AbilityConditionType)).Length];
    //Distance contidion node
    [SerializeField] EnemyDistanceCondition enemyDistanceCondition = new EnemyDistanceCondition();
    //Angle condition node
    [SerializeField] EnemyAngleCondition enemyAngleCondition = new EnemyAngleCondition();
   
    //Basic components

    private EnemyCharacter enemyCharacter;

    public void SetEnemyAbilityConditionsProperties(EnemyCharacter enemyCharacter)
    {
        this.enemyCharacter = enemyCharacter;
    }

    //A method that creates a condition sequence node
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
            enemyAngleCondition.SetEnemyAngleConditionProperties(GameController.instance.PlayerTransform, enemyCharacter.transform);
            conditionsEnemyAbilityChildren.Add(enemyAngleCondition);
        }

        return new Sequence(conditionsEnemyAbilityChildren);
       
    }

   
}
