using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : AbilityManager
{
   
    // Start is called before the first frame update
    [SerializeField] public PlayerAbility[] PlayerAbilities;
    private PlayerCharacter playerCharacter;


    // Update is called once per frame

    protected override void Awake()
    {
        base.Awake();
        playerCharacter = GetComponent<PlayerCharacter>();
        
        PlayerAbilities = GetComponents<PlayerAbility>();
        abilitiesCount = PlayerAbilities.Length;

    }
    protected override void Start()
    {
        base.Start();
        myStats = playerCharacter.PlayerStats;

    }

    public override void PerformAbility(int index)
    {
        PlayerAbilities[index].TriggeAbility();
    }
}
