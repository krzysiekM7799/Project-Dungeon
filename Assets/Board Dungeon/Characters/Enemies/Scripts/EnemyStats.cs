using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    //Basic components

    [SerializeField] private EnemyHpUI enemyHpUI;
    private EnemyCharacter enemyCharacter;
    
    //Properties

    public override int Hp { get => base.Hp; set { hp = value; enemyHpUI.SetHpValueToEnemyUI(Hp, MaxHp); } }

    protected override void Awake()
    {
        base.Awake();
        enemyCharacter = GetComponent<EnemyCharacter>();
        character = enemyCharacter;
    }

    protected override void Start()
    {
        base.Start();     
    }

}
