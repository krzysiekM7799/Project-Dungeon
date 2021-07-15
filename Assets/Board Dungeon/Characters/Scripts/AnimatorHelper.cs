using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorHelper : MonoBehaviour
{
    
    protected Animator animator;
    
    protected AbilityManager abilityManager;
    private Stats myStats;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        abilityManager = GetComponentInParent<AbilityManager>();
        animator = GetComponent<Animator>();
        myStats = GetComponentInParent<Stats>();
    }



    public void UseBasicAttack(int index)
    {
        abilityManager.MarkBasicAttack(index);
    }
    public void SetAttackColliderOn(int typeOfCollider)
    {
        abilityManager.StartDetectHit();
    }
    public void SetAttackColliderOff(int typeOfCollider)
    {
        abilityManager.StopDetectHit();
    }
 
    public void AutoTargetHit()
    {
        abilityManager.AutoTargetHit();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void OnAnimatorMove();
    
}
