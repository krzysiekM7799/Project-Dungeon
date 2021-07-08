using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int lvl;
    private int experience;
    private int hp;
    [SerializeField] private int maxHp;
    private int attackDmg;
    [SerializeField] private int maxAttackDmg;
    int abilityPower;
    [SerializeField] private int maxAbilityPower;
    private int armor;
    [SerializeField] private int maxArmor;
    int magicResist;
    [SerializeField] private int maxMagicResist;
    int criticalDmgChance;
    [SerializeField] private int maxCriticalDmgChance;
    int smashingDmgChance;
    private float currentPushDistance;
    public int Hp { get => hp; set => hp = value; }
    public int AttackDmg { get => attackDmg; set => attackDmg = value; }
    public int AbilityPower { get => abilityPower; set => abilityPower = value; }
    public int Armor { get => armor; set => armor = value; }
    public int MagicResist { get => magicResist; set => magicResist = value; }
    public int CriticalDmgChance { get => criticalDmgChance; set => criticalDmgChance = value; }
    public int SmashingDmgChance { get => smashingDmgChance; set => smashingDmgChance = value; }

    private bool died;
    private Character character;
    private float[] AbilityEffectsTimes;
    private bool characterCanBePushed = true;
    
    float realRadius;

    protected void Awake()
    {
        character = GetComponent<Character>();
        var AbilityEffectsCount = Enum.GetNames(typeof(AbilityEffect)).Length;
        AbilityEffectsTimes = new float[AbilityEffectsCount];
        
    }
    // Start is called before the first frame update
    void Start()
    {
        realRadius = character.GetRealRadiusRadius();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int attackDmg, int magicDmg)
    {
        int takenAttackDmg = ClampPositive(attackDmg - Armor);
        int takenMagicDmg = ClampPositive(magicDmg - MagicResist);
        int takenDmg = takenAttackDmg + takenMagicDmg;
        Hp -= takenDmg;
       // CheckIfDied();
    }
    public bool PushCharacter(Vector3 attackerPosition, float strengh = 1, bool relativeToAttackerPosition = false)
    {
        if (characterCanBePushed)
        {
            Vector3 pushVector = (transform.position - attackerPosition);
            pushVector.y = 0;
            pushVector = pushVector.normalized;
            //push distance depends on the player's position
            if (relativeToAttackerPosition)
            {
                var nearestColliderPointToAttacker = transform.position - pushVector * realRadius;
                strengh -= Vector3.Distance(nearestColliderPointToAttacker, attackerPosition);
            }
            Debug.Log(strengh);
            pushVector *= strengh;
            transform.position += pushVector;
            return true;
        }
      
        return false;
        
       
            
    }
    protected IEnumerator SetBuffAbilityDuration(StatType statType, float buffAbilityDuration, int buffStatValue)
    {


        switch (statType)
        {

            case StatType.AttackDmg:

                attackDmg += buffStatValue;
                yield return new WaitForSeconds(buffAbilityDuration);
                attackDmg -= buffStatValue;
                break;
            case StatType.AbilityPower:

                abilityPower += buffStatValue;
                yield return new WaitForSeconds(buffAbilityDuration);
                abilityPower -= buffStatValue;
                break;
            case StatType.Armor:

                armor += buffStatValue;
                yield return new WaitForSeconds(buffAbilityDuration);
                armor -= buffStatValue;

                break;
            case StatType.MagicResist:

                magicResist += buffStatValue;
                yield return new WaitForSeconds(buffAbilityDuration);
                magicResist -= buffStatValue;
                break;




        }



    }

    public void UseBuffAbility(StatType statType, float buffAbilityDuration, int buffStatValue)
    {
        
        switch (statType)
        {
            case StatType.Hp:
                if (hp + buffStatValue < maxHp)
                {
                    hp += buffStatValue;
                }
                else
                {
                    hp = maxHp;

                }

                break;
            default:
                StartCoroutine(SetBuffAbilityDuration(statType, buffAbilityDuration, buffStatValue));
                break;
            




        }



    }
    public void SetPushDistance(float pushDistance)
    {
        currentPushDistance = pushDistance;
    }
    void CheckIfDied()
    {
        if (Hp <= 0 && !died)
        {
            died = true;
          //  Die();
            Destroy(gameObject, 5f);
        }
    }
    public int ClampPositive(int value)
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
    public float ClampPositive(float value)
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

    void CheckAbilityEffects()
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

    void PerformAbilityEffect(AbilityEffect abilityEffect)
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

    private void FixedUpdate()
    {
        CheckAbilityEffects();
    }
}