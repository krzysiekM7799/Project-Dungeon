using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttackAbility : AttackAbility
{
    [SerializeField] PlayerAbilityProperties playerAbilityProperties;

    public override bool CheckAdditionalContidions()
    {
        if (abilityManager.GetAttackColliderStatus())
            return false;
        Debug.Log("Collider wylaczony");
        if (_CheckAdditionalConditions())
            return true;
       
        
        return false;

    }
    protected abstract bool _CheckAdditionalConditions();
   
    protected override void SetAbilityValues()
    {
        base.SetAbilityValues();
        _SetAbilityValues();
    }
    protected abstract void _SetAbilityValues();


    protected override void Start()
    {
        base.Start();
        playerAbilityProperties.OnAbilityUse = new CustomUnityEvents.FloatUnityEvent();
        _Start();

    }
    protected abstract void _Start();
    protected override void OnSuccessfulUse()
    {
        playerAbilityProperties.OnAbilityUse.Invoke(baseCooldownTimes[lvl]);
        _OnSuccessfulUse();
    }
    protected abstract void _OnSuccessfulUse();
}
