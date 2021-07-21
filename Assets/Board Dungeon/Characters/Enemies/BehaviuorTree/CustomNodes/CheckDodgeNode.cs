using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDodgeNode : BTNode
{
    private AbilityManager playerAbilityManger;
    private ComboManager playerComboManager;
    private EnemyCharacter enemyCharacter;
    private int percentChanceToDodge;
    private float timeBeetwenDodgeTries;
    private float lastTimeDodging;
    private ElapsedTimeChecker elapsedTimeChecker;


    public CheckDodgeNode(EnemyCharacter enemyCharacter, AbilityManager playerAbilityManger, ComboManager playerComboManager, int percentChanceToDodge, float timeBeetwenDodgeTries)
    {
        this.enemyCharacter = enemyCharacter;
        this.playerAbilityManger = playerAbilityManger;
        this.playerComboManager = playerComboManager;
        this.percentChanceToDodge = percentChanceToDodge;
        this.timeBeetwenDodgeTries = timeBeetwenDodgeTries;
        elapsedTimeChecker = new ElapsedTimeChecker(timeBeetwenDodgeTries);
        lastTimeDodging = -timeBeetwenDodgeTries;


    }

    public override NodeStates Evaluate()
    {
      if(elapsedTimeChecker.CheckElapsedTime())
        {
            if (((playerAbilityManger.UsingAbility && playerAbilityManger.CurrentAbilityType == AbilityType.AttackAbility) || playerComboManager.Attacking) && Vector3.Distance(enemyCharacter.transform.position, playerAbilityManger.transform.position) < 4f)
            {
                if (ThingCalculator.TryWithPercentChance(percentChanceToDodge))
                {

                    elapsedTimeChecker.StartCountTime();
                    return NodeStates.SUCCESS;
                }
                else
                {
                    elapsedTimeChecker.StartCountTime();
                    return NodeStates.FAILURE;
                }

                
            }
        }
       
        
        return NodeStates.FAILURE;
    }

    // Start is called before the first frame update
   
}
