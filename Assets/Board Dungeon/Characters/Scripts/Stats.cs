using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour
{
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
    [SerializeField] private ParticleSystem bloodParticles;
    
    private float currentPushDistance;
    public int Hp { get => hp; set => hp = value; }
    public int AttackDmg { get => attackDmg; set => attackDmg = value; }
    public int AbilityPower { get => abilityPower; set => abilityPower = value; }
    public int Armor { get => armor; set => armor = value; }
    public int MagicResist { get => magicResist; set => magicResist = value; }
    public int CriticalDmgChance { get => criticalDmgChance; set => criticalDmgChance = value; }
    public int MaxHp { get => maxHp; }

    protected bool died;
    protected Character character;
    protected float[] AbilityEffectsTimes;
    

    protected float realRadius;

    protected virtual void Awake()
    {
     
        var AbilityEffectsCount = Enum.GetNames(typeof(AbilityEffect)).Length;
        AbilityEffectsTimes = new float[AbilityEffectsCount];
        
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        realRadius = character.GetRealRadiusRadius();
        hp = maxHp;
        attackDmg = maxAttackDmg;
        abilityPower = maxAbilityPower;
        armor = maxArmor;
        magicResist = maxMagicResist;
        criticalDmgChance = maxCriticalDmgChance;
    }

    // Update is called once per frame
   

    public void TakeDmg(int attackDmg, int magicDmg)
    {
        int takenAttackDmg = ClampPositive(attackDmg - Armor);
        int takenMagicDmg = ClampPositive(magicDmg - MagicResist);
        int takenDmg = takenAttackDmg + takenMagicDmg;
        Hp -= takenDmg;
       if(bloodParticles != null)
        {
            bloodParticles.transform.rotation = UnityEngine.Random.rotation;
            bloodParticles.Play();
        }
        
            
       // CheckIfDied();
    }
    public bool PushCharacter(Vector3 attackerPosition, float strengh = 1, bool relativeToAttackerPosition = false)
    {
       return  character.PushCharacter(attackerPosition, strengh, relativeToAttackerPosition);
    }

     
    protected void CheckIfDied()
    {
        if (Hp <= 0 && !died)
        {
            died = true;
          //  Die();
            Destroy(gameObject, 5f);
        }
    }
    private int ClampPositive(int value)
    {
        if (value <= 0)
        {
            return 0;
        }
        else
        {
            return value;
        }
    }
    private float ClampPositive(float value)
    {
        if (value <= 0f)
        {
            return 0f;
        }
        else
        {
            return value;
        }
    }

    private void CheckAbilityEffects()
    {
        for (int i = 0; i < AbilityEffectsTimes.Length; i++)
        {
            AbilityEffectsTimes[i] = ClampPositive(AbilityEffectsTimes[i] - 1 * Time.deltaTime);
            if (AbilityEffectsTimes[i] != 0f)
            {
                PerformAbilityEffect((AbilityEffect)i);
            }
        }
    }

    private void PerformAbilityEffect(AbilityEffect abilityEffect)
    {
        switch (abilityEffect)
        {
            case AbilityEffect.Stun:
                {
                    Debug.Log("Odtwarzam Stuna");
                }
                break;
            case AbilityEffect.Slow:
                {
                    Debug.Log("Odtwarzam Slowa");
                }
                break;
            case AbilityEffect.Poison:
                {
                    Debug.Log("Odtwarzam Posisona");
                }
                break;
            case AbilityEffect.Blind:
                {
                    Debug.Log("Odtwarzam Blinda");
                    break;
                }
        }
    }

    public void SetAbilityEffect(AbilityEffect abilityEffect, float durationTime, float value = 0)
    {

        if (AbilityEffectsTimes[(int)abilityEffect] < durationTime)
        {
            AbilityEffectsTimes[(int)abilityEffect] = durationTime;
        }
        if (value != 0)
        {

        }

    }

    protected void FixedUpdate()
    {
        CheckAbilityEffects();
    }
}