using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    private EnemyCharacter enemyCharacter;
    


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        
    }
    protected override void Awake()
    {
        base.Awake();
        enemyCharacter = GetComponent<EnemyCharacter>();
        character = enemyCharacter;
    }

    

 
    private void Update()
    {
        
    }
    // Update is called once per frame

}
