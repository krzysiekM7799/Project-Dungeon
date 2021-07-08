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
    [SerializeField] private Ability[] abilities;
    private int abilitiesCount;
    public int AbilitiesCount { get => abilitiesCount;}
    
   
  
    private CurrentAbilityProperties currentAbilityProperties;
    private Stats myStats;
    private Character character;
    [SerializeField] private Transform currentTarget;

    static string useAbilityTrigger = "UseAbility";
    

    public bool UsingAbility { get; set; }
    
  /*  public CustomUnityEvents.FloatUnityEvent GetAbilitiesEvent(int index)
    {
       // return abilities[index].OnAbilityUse;
    }
    public Sprite GetAbilityImg(int index)
    {
        
        return abilities[index].Image;
        

        
    }*/
    public BasicAttackProperties CurrentBasicAttacksProperties {
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
    public void SetCurrentAttackAbilitiesProperties(int attackDmg,float attackDmgMultiplier , int abilityPower,float abilityPowerMultiplier , float strenghOfPush, AbilityEffect abilityEffect, float abilityEffectValue, float abilityEffectTime)
    {
        currentAbilityProperties.currentAbilityType = AbilityType.AttackAbility;
        UsingAbility = true;
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

    private void Awake()
    {
        abilities = GetComponents<Ability>();
        character = GetComponent<Character>();
        abilitiesCount = abilities.Length;
    }
    private void Start()
    {
        attackColliderWeaponParentTransform = attackColliders.attackColliderTransform.parent;
        myStats = GetComponent<Stats>();
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
        if (currentAbilityProperties.currentParentOfCollider == ParentOfCollider.None)
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
                        targetStats.TakeDmg(myStats.AttackDmg + currentAbilityProperties.attackDmg, myStats.AbilityPower + currentAbilityProperties.abilityPower);
                        if (currentAbilityProperties.strenghOfPush != 0)
                        {
                            targetStats.PushCharacter(transform.position, currentAbilityProperties.strenghOfPush, false);
                        }
                        if (currentAbilityProperties.abilityEffect != AbilityEffect.None)
                        {
                            targetStats.SetAbilityEffect(currentAbilityProperties.abilityEffect, currentAbilityProperties.abilityEffectTime, currentAbilityProperties.abilityEffectValue);
                        }
                    }
                }
                break;
        }
    }
    public void PerformAbility(int index)
    {
        abilities[index].TriggeAbility();
    }
    public void SetAttackColliderProperties(AttackColliderProperties attackColliderProperties)
    {
        switch (attackColliderProperties.parentOfColldier)
        {
            case ParentOfCollider.Weapon:
                {
                    attackColliders.attackColliderTransform.SetParent(attackColliderWeaponParentTransform);
                     currentAbilityProperties.currentParentOfCollider = ParentOfCollider.Weapon;
                }
                break;
            case ParentOfCollider.CharacterObject:
                {
                    attackColliders.attackColliderTransform.SetParent(transform);
                    currentAbilityProperties.currentParentOfCollider = ParentOfCollider.CharacterObject;

                }
                break;
            case ParentOfCollider.None:
                attackColliders.attackColliderTransform.SetParent(transform);
                currentAbilityProperties.currentParentOfCollider = ParentOfCollider.None;
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
