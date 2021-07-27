using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  PlayerAbility : Ability
{
    //There are additional properties in the structure that are needed for the player's skills, such as the skill picture and the event for displaying the cooldown in UI
    [SerializeField] public PlayerAbilityProperties playerAbilityProperties;
    //Determines whether the player can control the character's rotation during the skill being played
    [SerializeField] protected bool canCharacterRotateDuringAbility;
    //In the playercharacter there are references to most of the player's needed components
    protected PlayerCharacter playerCharacter;
    protected override bool CheckAdditionalContidions()
    {
        //Prevent weird transitions between combo and ability animations
        if (abilityManager.GetAttackColliderStatus())
            return false;
     
        return true;
    }

    protected override void OnSuccessfulUse()
    {
        playerCharacter.RotationEnabled = canCharacterRotateDuringAbility;
        //Raise the event to the UI of ability icons
        playerAbilityProperties.OnAbilityUse.Invoke(baseCooldownTimes[lvl]);
    }
   
    protected override void Awake()
    {
        base.Awake();
        playerCharacter = GetComponent<PlayerCharacter>();
        playerAbilityProperties.OnAbilityUse = new CustomUnityEvents.FloatUnityEvent();
    }

}
