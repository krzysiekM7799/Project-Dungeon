using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private AbilityManager abilityManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Stats targetStats = other.transform.GetComponent<Stats>();
            targetStats.TakeDmg(0, 0);
            targetStats.PushCharacter(transform.root.position, 2f);
        }
    }
}
