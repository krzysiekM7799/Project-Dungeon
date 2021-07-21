using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAbility : EnemyAbility
{
    [SerializeField] private AttackAbility attackAbility = new AttackAbility();

    protected override bool UseAbility()
    {
        return attackAbility.UseAttackAbility(lvl);
    }

   
    protected virtual void _SetAbilityValues()
    {

    }
    protected override void Start()
    {
        base.Start();
        
        
        attackAbility.SetAttackAbilityProperties(abilityManager, transform);


    }

    // Start is called before the first frame update


    // Update is called once per frame

}
