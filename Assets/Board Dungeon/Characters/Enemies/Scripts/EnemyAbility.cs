using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : Ability
{
    //The class withstood the available conditions that must be met for the enemy to use the ability
    [SerializeField] private EnemyAbilityConditions enemyAbilityConditions = new EnemyAbilityConditions();
    //Sequence that checks conditions in order
    private Sequence enemyAbilityConditionsSequence;
    //In the enemycharacter there are references to most of the enemy's needed components
    protected EnemyCharacter enemyCharacter;
 
    protected override void Awake()
    {
        base.Awake();
        enemyCharacter = GetComponent<EnemyCharacter>();
    }

    protected override void Start()
    {
        base.Start();
        //Sets up the necessary components to enemyAbilityConditions class
        enemyAbilityConditions.SetEnemyAbilityConditionsProperties(enemyCharacter);
        //Making sequence of conditions
        enemyAbilityConditionsSequence = enemyAbilityConditions.MakeEnemyAbilityConditionsNode();
    }

    protected override bool CheckAdditionalContidions()
    {
        //Sequence call, if any of the conditions is not met, it returns false
        if (enemyAbilityConditionsSequence.Evaluate() == NodeStates.FAILURE)
        {
            return false;
        }
        return true;
    }

    protected override void OnSuccessfulUse()
    {
        
    }

    //Method needed for the main enemy's behavior tree to determine if it has been successfully used
    public virtual NodeStates UseAbilityForNode()
    {
        if(TriggeAbility())
        return NodeStates.SUCCESS;

        return NodeStates.FAILURE;
    }

}
