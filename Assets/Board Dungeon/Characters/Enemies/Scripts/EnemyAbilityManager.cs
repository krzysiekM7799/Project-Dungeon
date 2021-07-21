using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityManager : AbilityManager
{
    [SerializeField] public EnemyAbility[] EnemyAbilities;
    EnemyCharacter enemyCharacter;
    protected override void Awake()
    {
        base.Awake();
        enemyCharacter = GetComponent<EnemyCharacter>();
       
        EnemyAbilities = GetComponents<EnemyAbility>();
        
        
       
        
        abilitiesCount = EnemyAbilities.Length;
    }
    protected override void Start()
    {
        base.Start();
        myStats = enemyCharacter.EnemyStats;
    }
    public override void PerformAbility(int index)
    {
        EnemyAbilities[index].TriggeAbility();
    }

    // Start is called before the first frame update


}
