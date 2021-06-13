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
    [SerializeField] private int maxSmashingDmgChance;
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
    public float[] AbilityEffectsTimes;



    protected void Awake()
    {
        character = GetComponent<Character>();
        var AbilityEffectsCount = Enum.GetNames(typeof(AbilityEffect)).Length;
        AbilityEffectsTimes = new float[AbilityEffectsCount];
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void PushCharacter(Vector3 attackerPosition, float strengh = 1)
    {
        Debug.Log("dotykam");
        Vector3 pushVector = (transform.position - attackerPosition);
        pushVector.y = 0;
        pushVector = pushVector.normalized;
        pushVector *= 30;
         //transform.position += pushVector;
        character.Rigidbody.AddForce(pushVector * 10f);
            
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

    public enum AbilityEffect
    {
        STUN,
        SLOW,
        POISON,
        BLIND,
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
            case AbilityEffect.STUN:
                {
                    Debug.Log("Odtwarzam Stuna");
                }
                break;
            case AbilityEffect.SLOW:
                {
                    Debug.Log("Odtwarzam Slowa");
                }
                break;
            case AbilityEffect.POISON:
                {
                    Debug.Log("Odtwarzam Posisona");
                }
                break;
            case AbilityEffect.BLIND:
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