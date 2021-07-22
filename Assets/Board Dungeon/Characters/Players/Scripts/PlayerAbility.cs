using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  PlayerAbility : Ability
{
    [SerializeField] public PlayerAbilityProperties playerAbilityProperties;
    protected PlayerCharacter playerCharacter;
    [SerializeField] protected bool canCharacterRotateDuringAbility;
    protected override bool CheckAdditionalContidions()
    {
        if (abilityManager.GetAttackColliderStatus())
            return false;
     
        return true;
    }

    protected override void OnSuccessfulUse()
    {
        playerCharacter.RotationEnabled = canCharacterRotateDuringAbility;
        playerAbilityProperties.OnAbilityUse.Invoke(baseCooldownTimes[lvl]);
    }
   
    protected override void Awake()
    {
        base.Awake();
        playerCharacter = GetComponent<PlayerCharacter>();
        playerAbilityProperties.OnAbilityUse = new CustomUnityEvents.FloatUnityEvent();
    }

}
