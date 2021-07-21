using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    
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
    // Update is called once per frame

}
