using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBuffAbility : BuffAbility
{
    
    protected override bool CheckAdditionalContidions()
    {
        if (abilityManager.GetAttackColliderStatus())
            return false;
        Debug.Log("Collider wylaczony");
        if (_CheckAdditionalConditions())
            return true;

        return false;
    }

    protected abstract bool _CheckAdditionalConditions();
   

    protected override void OnSuccessfulUse()
    {
        playerAbilityProperties.OnAbilityUse.Invoke(baseCooldownTimes[lvl]);
        _OnSuccessfulUse();
    }
    protected abstract void _OnSuccessfulUse();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _Start();

    }
    protected override void Awake()
    {
        base.Awake();
        playerAbilityProperties.OnAbilityUse = new CustomUnityEvents.FloatUnityEvent();
    }
    protected abstract void _Start();   
}
