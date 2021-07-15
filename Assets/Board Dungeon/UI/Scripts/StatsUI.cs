using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI attackDmgText;
    [SerializeField] private TextMeshProUGUI AbilityPowerText;
    public Stats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameController.instance.Player.GetComponent<Stats>();
        Invoke("SetTextValues", 1);

    }

    private void SetTextValues()
    {
        hpText.text = playerStats.Hp.ToString();
        attackDmgText.text = playerStats.AttackDmg.ToString();
    }

}
