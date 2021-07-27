using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    //Stats Texts

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI attackDmgText;
    [SerializeField] private TextMeshProUGUI abilityPowerText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI magicResistText;
    [SerializeField] private TextMeshProUGUI criticalChanceText;

    //Basic components

    private Stats stats;

    void Start()
    {
        stats = GameController.instance.PlayerTransform.GetComponent<Stats>();   
    }

    public void InvokeStatsChange(StatType statType)
    {
        SetTextValues(statType);
    }

    private void SetTextValues(StatType statType)
    {
        switch (statType)
        {
            case StatType.Hp:
                {
                    hpText.text = stats.Hp.ToString();
                }
                break;
            case StatType.AttackDmg:
                {
                    attackDmgText.text = stats.AttackDmg.ToString();
                }
                break;
            case StatType.AbilityPower:
                {
                    abilityPowerText.text = stats.AbilityPower.ToString();
                }
                break;
            case StatType.Armor:
                {
                    armorText.text = stats.Armor.ToString();
                }
                break;
            case StatType.MagicResist:
                {
                    magicResistText.text = stats.MagicResist.ToString();
                }
                break;
            case StatType.CriticalDmgChance:
                {
                    criticalChanceText.text = stats.CriticalDmgChance.ToString();
                }
                break;
        };             
    }

}
