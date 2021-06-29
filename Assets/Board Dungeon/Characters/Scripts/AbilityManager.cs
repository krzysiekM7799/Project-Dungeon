using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // Structure, which hold transform of object with colliders, colliders, and information which collider is current using
    [SerializeField] AttackColliders attackColliders;
    //Transform of the character's weapon parent
    Transform attackColliderWeaponParentTransform;
    //List of Basic Attack scriptable objects
    [SerializeField] private BasicAttack[] basicAttacks;
    [SerializeField] private AttackAbility[] attackAbilities;
    private int attackAbilitiesCount;
    public int AttackAbilitiesCount { get => attackAbilitiesCount;}
    [SerializeField] private AttackAbility[] buffAbilities;
    private int buffAbilitiesCount;
    public int BuffAbilitiesCount { get => buffAbilitiesCount; }
    private CurrentAbilityInformation currentAbilityInformation;
    private Stats myStats;
    private Character character;
    [SerializeField] private Transform currentTarget;
    static string useAbilityTrigger = "UseAbility";
    

    public bool UsingAbility { get; set; }

    public BasicAttackProperties currentBasicAttacksProperties {
        get
        {
            BasicAttackProperties basicAttackProperties;
            basicAttackProperties.attackDmgModifier = basicAttacks[currentAbilityInformation.currentAbilityIndex].attackDmgModifier;
            basicAttackProperties.strenghOfPush = basicAttacks[currentAbilityInformation.currentAbilityIndex].strenghOfPush;
            return basicAttackProperties;
        }
    }
    public AttackAbilityProperties currentAttackAbilityProperties
    {
        get
        {
            AttackAbilityProperties attackAbilityProperties;
            attackAbilityProperties.attackDmg = attackAbilities[currentAbilityInformation.currentAbilityIndex].attackDmg;
            attackAbilityProperties.abilityPower = attackAbilities[currentAbilityInformation.currentAbilityIndex].abilityPower;
            attackAbilityProperties.strenghOfPush = attackAbilities[currentAbilityInformation.currentAbilityIndex].strenghOfPush;
            attackAbilityProperties.abilityEffect = attackAbilities[currentAbilityInformation.currentAbilityIndex].abilityEffect;
            attackAbilityProperties.abilityEffectValue = attackAbilities[currentAbilityInformation.currentAbilityIndex].abilityEffectValue;
            attackAbilityProperties.abilityEffectTime = attackAbilities[currentAbilityInformation.currentAbilityIndex].abilityEffectTime;
            return attackAbilityProperties;

        }
    }

    public AbilityType CurrentAbilityType { get => currentAbilityInformation.currentAbilityType; set => currentAbilityInformation.currentAbilityType = value; }
    public CurrentAbilityInformation CurrentAbilityInformation { get => currentAbilityInformation; }
    private void Awake()
    {
        attackAbilitiesCount = attackAbilities.Length;
        buffAbilitiesCount = buffAbilities.Length;
        character = GetComponent<Character>();
    }
    private void Start()
    {
        attackColliderWeaponParentTransform = attackColliders.attackColliderTransform.parent;
        myStats = GetComponent<Stats>();
    }
    //Methods for enable or disable attack collider for animation events (non target attack abilities)
    public void StartDetectHit()
    {
        switch (currentAbilityInformation.currentColliderType)
        {
            case ColliderType.BoxCollider:
                attackColliders.attackBoxCollider.enabled = true;
                break;
            case ColliderType.SphereCollider:
                attackColliders.attackSphereCollider.enabled = true;
                break;

        }
        if (currentAbilityInformation.currentParentOfCollider == ParentOfCollider.None)
        {
            attackColliders.attackColliderTransform.SetParent(null);
        }


    }
    public void StopDetectHit()
    {
        switch (currentAbilityInformation.currentColliderType)
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

    public void MarkAttackAbility(int index)
    {
        UsingAbility = true;
        currentAbilityInformation.currentAbilityIndex = index;
        currentAbilityInformation.currentAbilityType = AbilityType.AttackAbility;
        SetAttackColliderProperties(attackAbilities[currentAbilityInformation.currentAbilityIndex]);
    }
    public void MarkBasicAttack(int index)
    {

        currentAbilityInformation.currentAbilityIndex = index;
        currentAbilityInformation.currentAbilityType = AbilityType.BasicAttack;
        SetAttackColliderProperties(basicAttacks[index]);
    }
    public void MarkAHit(Stats targetStats)
    {
        switch (CurrentAbilityInformation.currentAbilityType)
        {
            case AbilityType.BasicAttack:
                {
                    targetStats.TakeDmg(myStats.AttackDmg + currentBasicAttacksProperties.attackDmgModifier, 0);
                    if (currentBasicAttacksProperties.strenghOfPush != 0)
                    {
                        targetStats.PushCharacter(transform.position, currentBasicAttacksProperties.strenghOfPush, true);
                    }
                }
                break;
            case AbilityType.AttackAbility:
                {
                    targetStats.TakeDmg(myStats.AttackDmg + currentAttackAbilityProperties.attackDmg, myStats.AbilityPower + currentAttackAbilityProperties.abilityPower);
                    if (currentAttackAbilityProperties.strenghOfPush != 0)
                    {
                        targetStats.TakeDmg(myStats.AttackDmg + currentAttackAbilityProperties.attackDmg, myStats.AbilityPower + currentAttackAbilityProperties.abilityPower);
                        if (currentAttackAbilityProperties.strenghOfPush != 0)
                        {
                            targetStats.PushCharacter(transform.position, currentAttackAbilityProperties.strenghOfPush, false);
                        }
                        if (currentAttackAbilityProperties.abilityEffect != AbilityEffect.None)
                        {
                            targetStats.SetAbilityEffect(currentAttackAbilityProperties.abilityEffect, currentAttackAbilityProperties.abilityEffectTime, currentAttackAbilityProperties.abilityEffectValue);
                        }
                    }
                }
                break;
        }
    }
    public bool PerformAttackAbility(int index)
    {
        if (attackAbilities[index].minDistanceToUse == 0) {
            
            character.SetAnimatorParametr(AnimatorParametrType.Trigger, useAbilityTrigger + index);
            return true;
        }
        else if (attackAbilities[index].minDistanceToUse <= Vector3.Distance(currentTarget.position, transform.position))
        {
            character.SetAnimatorParametr(AnimatorParametrType.Trigger, useAbilityTrigger + index);
            return true;
        }

        return false;
    }
    public bool PerformBuffAbility(int index)
    {
        return false;
    }
    public bool PerformAbility(int index)
    {
        Debug.Log("uzywam um " + index);
        
        if(index > attackAbilities.Length - 1)
        {
            index -= attackAbilities.Length;
            return PerformBuffAbility(index);
        }
        else
        {
            return PerformAttackAbility(index);
            
        }
     

    }

    private void SetAttackColliderProperties(BasicAttack ability)
    {
        switch (ability._attackColliderProperties.parentOfColldier)
        {
            case ParentOfCollider.Weapon:
                {
                    attackColliders.attackColliderTransform.SetParent(attackColliderWeaponParentTransform);
                    currentAbilityInformation.currentParentOfCollider = ParentOfCollider.Weapon;
                }
                break;
            case ParentOfCollider.CharacterObject:
                {
                    attackColliders.attackColliderTransform.SetParent(transform);
                    currentAbilityInformation.currentParentOfCollider = ParentOfCollider.CharacterObject;

                }
                break;
            case ParentOfCollider.None:
                attackColliders.attackColliderTransform.SetParent(transform);
                currentAbilityInformation.currentParentOfCollider = ParentOfCollider.None;
                break;
        }
        attackColliders.attackColliderTransform.localPosition = ability._attackColliderProperties.positionOfCollider;
        attackColliders.attackColliderTransform.localRotation = ability._attackColliderProperties.rotationOfCollider;
        currentAbilityInformation.currentColliderType = ability._attackColliderProperties.colliderType;
        switch (ability._attackColliderProperties.colliderType)
        {
            case ColliderType.BoxCollider:
                {
                    attackColliders.attackBoxCollider.center = ability._attackColliderProperties.centerOfCollider;
                    attackColliders.attackBoxCollider.size = ability._attackColliderProperties.sizeOfCollider;
                }
                break;
            case ColliderType.SphereCollider:
                {
                    attackColliders.attackSphereCollider.center = ability._attackColliderProperties.centerOfCollider;
                    attackColliders.attackSphereCollider.radius = ability._attackColliderProperties.radiusOfCollider;
                }
                break;
        }
    }





}
