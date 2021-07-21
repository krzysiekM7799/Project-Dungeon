using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : Ability
{
    protected EnemyCharacter enemyCharacter;
    [SerializeField] private EnemyAbilityConditions enemyAbilityConditions = new EnemyAbilityConditions();
    private Sequence enemyAbilityConditionsSequence;
    protected override bool CheckAdditionalContidions()
    {
        if(enemyAbilityConditionsSequence.Evaluate() == NodeStates.FAILURE)
        {
            return false;
        }
        return true;
    }

    protected override void OnSuccessfulUse()
    {
       
    }

    protected override void Awake()
    {
        base.Awake();
        enemyCharacter = GetComponent<EnemyCharacter>();
    }
    protected override void Start()
    {
        base.Start();
        enemyAbilityConditions.SetEnemyAbilityConditionsProperties(enemyCharacter);
        enemyAbilityConditionsSequence = enemyAbilityConditions.MakeEnemyAbilityConditionsNode();
    }
    public virtual NodeStates UseAbilityForNode()
    {

        if(TriggeAbility())
        return NodeStates.SUCCESS;

        return NodeStates.FAILURE;
    }
   

}
