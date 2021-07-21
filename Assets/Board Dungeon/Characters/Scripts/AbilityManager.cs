using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityManager : MonoBehaviour
{
    // Structure, which hold transform of object with colliders, colliders, and information which collider is current using
    [SerializeField] AttackColliders attackColliders;

    //List of Basic Attack scriptable objects
    [SerializeField] protected BasicAttack[] basicAttacks;

    protected int abilitiesCount;
    public int AbilitiesCount { get => abilitiesCount; }
    protected CurrentAbilityProperties currentAbilityProperties;
    protected Stats myStats;
    [SerializeField] private Transform currentTarget;

    public bool UsingAbility { get; set; }

    public BasicAttackProperties CurrentBasicAttacksProperties
    {
        get
        {
            BasicAttackProperties basicAttackProperties;
            basicAttackProperties.attackDmgModifier = basicAttacks[currentAbilityProperties.currentAbilityIndex].attackDmgModifier;
            basicAttackProperties.strenghOfPush = basicAttacks[currentAbilityProperties.currentAbilityIndex].strenghOfPush;
            return basicAttackProperties;
        }
    }

    public AttackAbilityProperties GetCurrentAttackAbilitiesProperties()
    {
        AttackAbilityProperties attackAbilityProperties;
        attackAbilityProperties.attackDmg = currentAbilityProperties.attackDmg;
        attackAbilityProperties.abilityPower = currentAbilityProperties.abilityPower;
        attackAbilityProperties.strenghOfPush = currentAbilityProperties.strenghOfPush;
        attackAbilityProperties.abilityEffect = currentAbilityProperties.abilityEffect;
        attackAbilityProperties.abilityEffectValue = currentAbilityProperties.abilityEffectValue;
        attackAbilityProperties.abilityEffectTime = currentAbilityProperties.abilityEffectTime;
        return attackAbilityProperties;
    }
    public void SetCurrentAttackAbilitysProperties(int attackDmg, float attackDmgMultiplier, int abilityPower, float abilityPowerMultiplier, float strenghOfPush, AbilityEffect abilityEffect, float abilityEffectValue, float abilityEffectTime)
    {
        currentAbilityProperties.currentAbilityType = AbilityType.AttackAbility;
        currentAbilityProperties.attackDmg = attackDmg;
        currentAbilityProperties.attackDmgMultiplier = attackDmgMultiplier;
        currentAbilityProperties.abilityPower = abilityPower;
        currentAbilityProperties.abilityPowerMultiplier = abilityPowerMultiplier;
        currentAbilityProperties.strenghOfPush = strenghOfPush;
        currentAbilityProperties.abilityEffect = abilityEffect;
        currentAbilityProperties.abilityEffectValue = abilityEffectValue;
        currentAbilityProperties.abilityEffectTime = abilityEffectTime;
    }

    public bool GetAttackColliderStatus()
    {
        if (attackColliders.attackBoxCollider.enabled == true)
            return true;
        if (attackColliders.attackSphereCollider.enabled == true)
            return true;
        return false;

    }

    public AbilityType CurrentAbilityType { get => currentAbilityProperties.currentAbilityType; set => currentAbilityProperties.currentAbilityType = value; }
    public CurrentAbilityProperties CurrentAbilityProperties { get => currentAbilityProperties; }
    public Transform CurrentTarget { get => currentTarget; set => currentTarget = value; }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    //Methods for enable or disable attack collider for animation events (non target attack abilities)
    public void StartDetectHit()
    {
        switch (currentAbilityProperties.currentColliderType)
        {
            case ColliderType.BoxCollider:
                attackColliders.attackBoxCollider.enabled = true;
                break;
            case ColliderType.SphereCollider:
                attackColliders.attackSphereCollider.enabled = true;
                break;

        }
        if (currentAbilityProperties.currentParentOfCollider == ParentOfObject.None)
        {
            attackColliders.attackColliderTransform.SetParent(null);
        }
    }

    public void StopDetectHit()
    {
        switch (currentAbilityProperties.currentColliderType)
        {
            case ColliderType.BoxCollider:
                attackColliders.attackBoxCollider.enabled = false;
                break;
            case ColliderType.SphereCollider:
                attackColliders.attackSphereCollider.enabled = false;
                break;
        }
    }

    public void AutoTargetHit()
    {
        MarkAHit(currentTarget.GetComponent<Stats>());
    }

    public void MarkBasicAttack(int index)
    {
        currentAbilityProperties.currentAbilityIndex = index;
        currentAbilityProperties.currentAbilityType = AbilityType.BasicAttack;
        SetAttackColliderProperties(basicAttacks[index]._attackColliderProperties);
    }

    public void MarkAHit(Stats targetStats)
    {
        switch (CurrentAbilityProperties.currentAbilityType)
        {
            case AbilityType.BasicAttack:
                {
                    targetStats.TakeDmg(myStats.AttackDmg + CurrentBasicAttacksProperties.attackDmgModifier, 0);
                    if (CurrentBasicAttacksProperties.strenghOfPush != 0)
                    {
                        targetStats.PushCharacter(transform.position, CurrentBasicAttacksProperties.strenghOfPush, true);
                    }
                }
                break;
            case AbilityType.AttackAbility:
                {


                    targetStats.TakeDmg(currentAbilityProperties.attackDmg + (int)(myStats.AttackDmg * currentAbilityProperties.attackDmgMultiplier), currentAbilityProperties.abilityPower + (int)(myStats.AbilityPower * currentAbilityProperties.abilityPowerMultiplier));

                    if (currentAbilityProperties.strenghOfPush != 0)
                    {
                        targetStats.PushCharacter(transform.position, currentAbilityProperties.strenghOfPush, false);
                    }
                    if (currentAbilityProperties.abilityEffect != AbilityEffect.None)
                    {

                        targetStats.SetAbilityEffect(currentAbilityProperties.abilityEffect, currentAbilityProperties.abilityEffectTime, currentAbilityProperties.abilityEffectValue);
                    }

                }
                break;
        }
    }

    public abstract void PerformAbility(int index);

    public void SetAttackColliderProperties(AttackColliderProperties attackColliderProperties)
    {
        switch (attackColliderProperties.parentOfColldier)
        {

            case ParentOfObject.CharacterObject:
                {
                    attackColliders.attackColliderTransform.SetParent(transform);
                    currentAbilityProperties.currentParentOfCollider = ParentOfObject.CharacterObject;

                }
                break;
            case ParentOfObject.None:
                attackColliders.attackColliderTransform.SetParent(transform);
                currentAbilityProperties.currentParentOfCollider = ParentOfObject.None;
                break;
        }

        attackColliders.attackColliderTransform.localPosition = attackColliderProperties.positionOfCollider;
        attackColliders.attackColliderTransform.localRotation = attackColliderProperties.rotationOfCollider;
        currentAbilityProperties.currentColliderType = attackColliderProperties.colliderType;
        switch (attackColliderProperties.colliderType)
        {
            case ColliderType.BoxCollider:
                {
                    attackColliders.attackBoxCollider.center = attackColliderProperties.centerOfCollider;
                    attackColliders.attackBoxCollider.size = attackColliderProperties.sizeOfCollider;
                }
                break;
            case ColliderType.SphereCollider:
                {
                    attackColliders.attackSphereCollider.center = attackColliderProperties.centerOfCollider;
                    attackColliders.attackSphereCollider.radius = attackColliderProperties.radiusOfCollider;
                }
                break;
        }
    }
}
