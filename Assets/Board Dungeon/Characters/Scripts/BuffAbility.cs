using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffAbility : Ability
{
    protected StatType buffStatType;
    protected int[] buffStatValue = new int[3];
    protected float[] buffStatDuration = new float[3];
    protected Stats myStats;

    protected virtual void Start()
    {
        myStats = GetComponent<Stats>();
    }

    protected override bool UseAbility()
    {
        return BasicBuffAbility();
    }
    protected bool BasicBuffAbility()
    {
        BaseUseAbility();
        myStats.UseBuffAbility(buffStatType, buffStatDuration[lvl], buffStatValue[lvl]);
        return true;
    }
}
