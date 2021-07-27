using UnityEngine;

public class PlayerBuffAbility : PlayerAbility
{
    [SerializeField] protected BuffAbility buffAbility = new BuffAbility();
    
    //Basic components

    private PlayerStats myStats;

    protected override void Awake()
    {
        base.Awake();
        myStats = GetComponent<PlayerStats>();
    }

    protected override void Start()
    {
        base.Start();     
        buffAbility.SetBuffAbilityProperties(myStats, abilityManager, particle);
    }

    protected override bool UseAbility()
    {
        StartCoroutine(buffAbility.UseBuffAbility(lvl));
        return true;
    }
}
