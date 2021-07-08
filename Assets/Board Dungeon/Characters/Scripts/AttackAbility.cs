using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AttackAbility : Ability
{
    [SerializeField] protected int[] attackDmg = new int[3];
    [SerializeField] protected float attackDmgMultiplier;
    [SerializeField] protected int[] abilityPower = new int[3];
    [SerializeField] protected float abilityPowerMultiplier;
    [SerializeField] protected bool isAutoTarget;
    [SerializeField] protected float minDistanceToUse;
    [SerializeField] protected AbilityEffect abilityEffect;
    [SerializeField] protected float[] abilityEffectValue = new float[3];
    [SerializeField] protected float[] abilityEffectTime = new float[3];
    [SerializeField] protected float[] strenghOfPush = new float[1];
    [SerializeField] protected AttackColliderProperties attackColliderProperties;
    [SerializeField] protected Animation attackColliderAnimation;
    [SerializeField] protected string animationName; 
    


    protected virtual void Start()
    {
        abilityType = AbilityType.AttackAbility;
    }
   

    protected override bool UseAbility()
    {
        return BasicAttackAbility();
    }
   
    protected bool BasicAttackAbility()
    {
        if (!isAutoTarget)
        {
            Debug.Log("jestem");
            BaseUseAbility();
            return true;
        }
        else if (minDistanceToUse >= Vector3.Distance(abilityManager.CurrentTarget.position, transform.position))
        {
            var lookDirection = abilityManager.CurrentTarget.position;
            lookDirection.y = transform.position.y;
            transform.LookAt(lookDirection);
            BaseUseAbility();
            return true;
        }
        return false;
    }
    protected override void BaseUseAbility()
    {
        base.BaseUseAbility();
        if (attackColliderAnimation != null)
            attackColliderAnimation.Play(animationName);
    }

    protected override void SetAbilityValues()
    {
        abilityManager.SetCurrentAttackAbilitiesProperties(attackDmg[lvl], attackDmgMultiplier ,abilityPower[lvl],abilityPowerMultiplier ,(strenghOfPush.Length - 1  < lvl) ? strenghOfPush[strenghOfPush.Length -1] : strenghOfPush[lvl], abilityEffect, abilityEffectValue[lvl], abilityEffectTime[lvl]);
        if(!isAutoTarget)
        abilityManager.SetAttackColliderProperties(attackColliderProperties);
    }
   
}
