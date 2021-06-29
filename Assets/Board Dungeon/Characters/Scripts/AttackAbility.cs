using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackAbility", menuName = "Abilities/AttackAbility")]
public class AttackAbility : BasicAttack
{
    //public int attackDmgModifier;
    //public float strenghOfPush;
    //public AttackColliderProperties _attackColliderProperties;
    public int attackDmg;
    public float attackDmgMultiplier;
    public int abilityPower;
    public float abilityPowerMultiplier;
    public float cooldownTime;
    public float[] cooldownMultiplier = new float[3];
    public bool isAutoTargert;
    public float minDistanceToUse;
    public AbilityEffect abilityEffect;
    public float abilityEffectValue;
    public float abilityEffectTime;
    public float durationTimeAfterColliderOn;


}