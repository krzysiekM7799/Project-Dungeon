using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAbility : PlayerAbility
{
    
   [SerializeField] protected AttackAbility attackAbility = new AttackAbility();
    
    protected override void Start()
    {
        base.Start();      
        attackAbility.SetAttackAbilityProperties(abilityManager, transform);
    }

    protected override void Awake()
    {
        base.Awake();   
    }

    protected override bool UseAbility()
    {
        return attackAbility.UseAttackAbility(lvl);
    }
}
