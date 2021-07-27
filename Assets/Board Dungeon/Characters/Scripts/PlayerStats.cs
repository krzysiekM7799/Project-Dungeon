using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    [SerializeField] private StatsUI statsUI;
    public override int Hp { get => base.Hp; set { hp = value; statsUI.InvokeStatsChange(StatType.Hp); } }
    public override int AttackDmg { get => base.AttackDmg; set { attackDmg = value; statsUI.InvokeStatsChange(StatType.AttackDmg); } }
    public override int AbilityPower { get => base.AbilityPower; set { abilityPower = value; statsUI.InvokeStatsChange(StatType.AbilityPower); } }
    public override int Armor { get => base.Armor; set { armor = value; statsUI.InvokeStatsChange(StatType.Armor); } }
    public override int MagicResist { get => base.MagicResist; set { magicResist = value; statsUI.InvokeStatsChange(StatType.MagicResist); } }
    public override int CriticalDmgChance { get => base.CriticalDmgChance; set { criticalDmgChance = value; statsUI.InvokeStatsChange(StatType.CriticalDmgChance); } }

    private PlayerCharacter playerCharacter;
    protected override void Start()
    {
        base.Start();

    }
    protected override void Awake()
    {
        base.Awake();
        playerCharacter = GetComponent<PlayerCharacter>();

        character = playerCharacter;
    }

    public override void TakeDmg(int attackDmg, int magicDmg)
    {
        base.TakeDmg(attackDmg, magicDmg);
    }
    // Update is called once per frame

}
