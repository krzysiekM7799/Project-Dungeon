using UnityEngine;

[System.Serializable]
public class AttackAbility
{
    [SerializeField] protected int[] attackDmg = new int[1];
    [SerializeField] protected float attackDmgMultiplier;
    [SerializeField] protected int[] abilityPower = new int[1];
    [SerializeField] protected float abilityPowerMultiplier;
    [SerializeField] protected bool isAutoTarget;
    [SerializeField] protected float minDistanceToUse;
    [SerializeField] protected AbilityEffect abilityEffect = AbilityEffect.None;
    [SerializeField] protected float[] abilityEffectValue = new float[1];
    [SerializeField] protected float[] abilityEffectTime = new float[1];
    [SerializeField] protected float[] strenghOfPush = new float[1];
    [SerializeField] protected AttackColliderProperties attackColliderProperties;
    [SerializeField] protected Animation attackColliderAnimation;
    [SerializeField] protected string animationName;
    private AbilityManager abilityManager;
    private Transform transform;

    //Have to set it by setter (not constructor), to take information about properties from editor
    public void SetAttackAbilityProperties(AbilityManager abilityManager, Transform transform)
    {
        this.abilityManager = abilityManager;
        this.transform = transform;
    }

    public bool UseAttackAbility(int abilityLvl)
    {
        if (!isAutoTarget)
        {
            SetAttackAbilityValues(abilityLvl);
            Debug.Log("jestem");
            AttackColliderAnimation();
            return true;
        }
        else if (minDistanceToUse >= Vector3.Distance(abilityManager.CurrentTarget.position, transform.position))
        {
            SetAttackAbilityValues(abilityLvl);
            var lookDirection = abilityManager.CurrentTarget.position;
            lookDirection.y = transform.position.y;
            transform.LookAt(lookDirection);
            AttackColliderAnimation();
            return true;
        }
        return false;
    }
    public void AttackColliderAnimation()
    {     
        if (attackColliderAnimation != null)
            attackColliderAnimation.Play(animationName);
    }

    public  void SetAttackAbilityValues(int abilityLvl)
    {
        abilityManager.SetCurrentAttackAbilitysProperties(attackDmg[ThingCalculator.CheckAbilityLvl(attackDmg.Length, abilityLvl)],
            attackDmgMultiplier, 
            abilityPower[ThingCalculator.CheckAbilityLvl(abilityPower.Length,abilityLvl)], 
            abilityPowerMultiplier, 
            strenghOfPush[ThingCalculator.CheckAbilityLvl(strenghOfPush.Length,abilityLvl)],
            abilityEffect, 
            abilityEffectValue[ThingCalculator.CheckAbilityLvl(abilityEffectValue.Length, abilityLvl)], 
            abilityEffectTime[ThingCalculator.CheckAbilityLvl(abilityEffectTime.Length, abilityLvl)]);
        if(!isAutoTarget)
        abilityManager.SetAttackColliderProperties(attackColliderProperties);
    }
}
