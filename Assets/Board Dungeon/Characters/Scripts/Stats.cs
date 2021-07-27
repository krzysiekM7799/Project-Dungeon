using System;
using UnityEngine;

//Class responsible for character statistics
public abstract class Stats : MonoBehaviour
{
    //Stats
    [SerializeField] private int lvl;
    protected int experience;
    protected int hp;
    [SerializeField] protected int maxHp;
    protected int attackDmg;
    [SerializeField] protected int maxAttackDmg;
    protected int abilityPower;
    [SerializeField] protected int maxAbilityPower;
    protected int armor;
    [SerializeField] protected int maxArmor;
    protected int magicResist;
    [SerializeField] protected int maxMagicResist;
    protected int criticalDmgChance;
    [SerializeField] protected int maxCriticalDmgChance;

    //Blood particles, it plays when character take dmg
    [SerializeField] private ParticleSystem bloodParticles;
    //Array which holds effects time left
    protected float[] AbilityEffectsTimes;
    //If enemy is dead
    protected bool died;
    
    //Basic componentes

    protected Character character;
 

    //Properties

    public virtual int Hp { get => hp; set => hp = value; }
    public virtual int AttackDmg { get => attackDmg; set => attackDmg = value; }
    public virtual int AbilityPower { get => abilityPower; set => abilityPower = value; }
    public virtual int Armor { get => armor; set => armor = value; }
    public virtual int MagicResist { get => magicResist; set => magicResist = value; }
    public virtual int CriticalDmgChance { get => criticalDmgChance; set => criticalDmgChance = value; }
    public virtual int MaxHp { get => maxHp; }

   

    protected virtual void Awake()
    {
     
        var AbilityEffectsCount = Enum.GetNames(typeof(AbilityEffect)).Length;
        AbilityEffectsTimes = new float[AbilityEffectsCount];
    }

    protected virtual void Start()
    {
        Hp = maxHp;
        AttackDmg = maxAttackDmg;
        AbilityPower = maxAbilityPower;
        Armor = maxArmor;
        MagicResist = maxMagicResist;
        CriticalDmgChance = maxCriticalDmgChance;
    }

    //Main take dmg method
    public virtual void TakeDmg(int attackDmg, int magicDmg)
    {
        int takenAttackDmg = ThingCalculator.ClampPositive(attackDmg - Armor);
        int takenMagicDmg = ThingCalculator.ClampPositive(magicDmg - MagicResist);
        int takenDmg = takenAttackDmg + takenMagicDmg;
        Hp -= takenDmg;
       if(bloodParticles != null)
        {
            bloodParticles.transform.rotation = UnityEngine.Random.rotation;
            bloodParticles.Play();
        }
    }

    //Trigge push character
    public bool PushCharacter(Vector3 attackerPosition, float strengh = 1, bool relativeToAttackerPosition = false)
    {
       return  character.PushCharacter(attackerPosition, strengh, relativeToAttackerPosition);
    }
  
    protected void CheckIfDied()
    {
        if (Hp <= 0 && !died)
        {
            died = true;
            Destroy(gameObject, 5f);
        }
    }

    //Check if if any abilit effect is active on character
    private void CheckAbilityEffects()
    {
        for (int i = 0; i < AbilityEffectsTimes.Length; i++)
        {
            AbilityEffectsTimes[i] = ThingCalculator.ClampPositive(AbilityEffectsTimes[i] - 1 * Time.deltaTime);
            if (AbilityEffectsTimes[i] != 0f)
            {
                PerformAbilityEffect((AbilityEffect)i);
            }
        }
    }

    //A method waiting for the logic of individual effects to be added
    private void PerformAbilityEffect(AbilityEffect abilityEffect)
    {
        switch (abilityEffect)
        {
            case AbilityEffect.Stun:
                {
                    Debug.Log("Stun");
                }
                break;
            case AbilityEffect.Slow:
                {
                    Debug.Log("Slow");
                }
                break;
            case AbilityEffect.Poison:
                {
                    Debug.Log("Poison");
                }
                break;
            case AbilityEffect.Blind:
                {
                    Debug.Log("Blind");
                    break;
                }
        }
    }

    //Metoda to sets up ability effect by attacker
    public void SetAbilityEffect(AbilityEffect abilityEffect, float durationTime, float value = 0)
    {

        if (AbilityEffectsTimes[(int)abilityEffect] < durationTime)
        {
            AbilityEffectsTimes[(int)abilityEffect] = durationTime;
        }
    }

    protected void FixedUpdate()
    {
        CheckAbilityEffects();
    }
}