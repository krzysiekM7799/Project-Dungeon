using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBuffAbility : BuffAbility
{
    PlayerAbilityProperties playerAbilityProperties;
    public override bool CheckAdditionalContidions()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnSuccessfulUse()
    {
        playerAbilityProperties.OnAbilityUse.Invoke(baseCooldownTimes[lvl]);
        _OnSuccessfulUse();
    }
    protected abstract void _OnSuccessfulUse();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerAbilityProperties.OnAbilityUse = new CustomUnityEvents.FloatUnityEvent();
        _Start();

    }
    protected abstract void _Start();   
}
