using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffAbility
{
    //Buff ability properties
    [SerializeField] protected StatType buffStatType;
    [SerializeField] protected int[] buffStatValue = new int[1];
    [SerializeField] protected float[] buffStatDuration = new float[1];
    
    //Basic components
    
    protected Stats myStats;
    protected AbilityManager abilityManager;
    ParticleSystem abilityParticle;

    //Set needed compoments
    public void SetBuffAbilityProperties(Stats myStats, AbilityManager abilityManager, ParticleSystem abilityParticle = null)
    {
        this.myStats = myStats;
        this.abilityManager = abilityManager;
        this.abilityParticle = abilityParticle;
    }

    //Buff abilities are based on coroutine and work on character's stats
    public IEnumerator UseBuffAbility(int abilityLvl)
    {
        SetBuffAbilityValues();
        var currentBuffStatDuration = buffStatDuration[ThingCalculator.CheckAbilityLvl(buffStatDuration.Length, abilityLvl)];
        var currentBuffStatValue = buffStatValue[ThingCalculator.CheckAbilityLvl(buffStatValue.Length, abilityLvl)];

        switch (buffStatType)
        {
            case StatType.Hp:
                {
                    if (myStats.Hp + currentBuffStatValue < myStats.MaxHp)
                    {
                        myStats.Hp += currentBuffStatValue;
                    }
                    else
                    {
                        myStats.Hp = myStats.MaxHp;

                    }
                    yield return 0;
                    break;
                }
            case StatType.AttackDmg:
                {
                    myStats.AttackDmg += currentBuffStatValue;
                    yield return new WaitForSeconds(currentBuffStatDuration);
                    myStats.AttackDmg -= currentBuffStatValue;
                    SetParticleOff();
                    break;
                }
            case StatType.AbilityPower:
                {
                    myStats.AbilityPower += currentBuffStatValue;
                    yield return new WaitForSeconds(currentBuffStatDuration);
                    myStats.AbilityPower -= currentBuffStatValue;
                    SetParticleOff();
                    break;
                }
            case StatType.Armor:
                {
                    myStats.Armor += currentBuffStatValue;
                    yield return new WaitForSeconds(currentBuffStatDuration);
                    myStats.Armor -= currentBuffStatValue;
                    SetParticleOff();
                    break;
                }
            case StatType.MagicResist:
                {
                    myStats.MagicResist += currentBuffStatValue;
                    yield return new WaitForSeconds(currentBuffStatDuration);
                    myStats.MagicResist -= currentBuffStatValue;
                    SetParticleOff();
                    break;
                }
        }
    }

    //lets the ability manager know that character is using a buff ability
    public void SetBuffAbilityValues()
    {
        abilityManager.CurrentAbilityType = AbilityType.BuffAbility;
            
    }
    
    private void SetParticleOff(){
        if(abilityParticle != null)
        {
            abilityParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
