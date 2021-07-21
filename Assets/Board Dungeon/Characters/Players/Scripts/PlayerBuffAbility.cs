using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffAbility : PlayerAbility
{
    [SerializeField] protected BuffAbility buffAbility = new BuffAbility();
    private PlayerStats myStats;

    protected override void Start()
    {
        base.Start();     
        buffAbility.SetBuffAbilityProperties(myStats, abilityManager, particle);
    }

    protected override void Awake()
    {
        base.Awake();
        myStats = GetComponent<PlayerStats>();       
    }
    protected override bool UseAbility()
    {
        StartCoroutine(buffAbility.UseBuffAbility(lvl));
        return true;
    }
}
