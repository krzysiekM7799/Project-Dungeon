using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffAbility : EnemyAbility
{
    [SerializeField] protected BuffAbility buffAbility = new BuffAbility();
    private EnemyStats myStats;
    protected override bool UseAbility()
    {
        StartCoroutine(buffAbility.UseBuffAbility(lvl));
        return true;
    }

    protected override void Start()
    {
        base.Start();
        buffAbility.SetBuffAbilityProperties(myStats, abilityManager, particle);
    }
    protected override void Awake()
    {
        base.Awake();
        myStats = GetComponent<EnemyStats>();
    }
    
}
