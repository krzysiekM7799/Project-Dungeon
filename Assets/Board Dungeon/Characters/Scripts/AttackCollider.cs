using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private AbilityManager abilityManager;
    private void Start()
    {
        abilityManager = transform.root.GetComponent<AbilityManager>();
    }
    protected virtual bool CheckTag(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CameraEffects.ShakeOnce(0.3f);
            return true;
        }
        return false;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (CheckTag(other))
        {
            Debug.Log("HIT");
            Stats targetStats = other.transform.GetComponent<Stats>();
            CameraEffects.ShakeOnce(0.3f);
            
            abilityManager.MarkAHit(targetStats);
        }
    }
}
