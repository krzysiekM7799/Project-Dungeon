using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : AttackCollider
{

    protected override bool CheckTag(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            return true;
        }
        return false;
    }
}
