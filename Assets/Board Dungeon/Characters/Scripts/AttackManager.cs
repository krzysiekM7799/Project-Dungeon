using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private AbilityManager abilityManager;
    private void Start()
    {
        abilityManager = transform.root.GetComponent<AbilityManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("HIT");
            Stats targetStats = other.transform.GetComponent<Stats>();
            CameraEffects.ShakeOnce(0.3f);
            
            abilityManager.MarkAHit(targetStats);
        }
    }
}
