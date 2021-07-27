using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Basic components

    protected Animator animator;
    protected CapsuleCollider m_Capsule;
    protected AbilityManager abilityManager;

    //Movement properties

    public float MaxSpeed { get; set; }
    protected bool rotationEnabled = true;
    public bool RotationEnabled { get => rotationEnabled; set => rotationEnabled = value; }

    //If charracter can be pushed
    public bool CharacterCanBePushed = true;
    //Here the true radius of the character's capsule is held
    protected float realRadius;

    //Properties

    public Animator Animator { get => animator; set => animator = value; }

    public float GetRealRadiusRadius()
    {
        return m_Capsule.radius * transform.localScale.x;
    }
 
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        m_Capsule = GetComponent<CapsuleCollider>();
        abilityManager = GetComponent<AbilityManager>();
        realRadius = GetRealRadiusRadius(); 
    
    }
    //Method that allows you to push the character away while receiving hit over a certain distance (relative to the attacking character or not)
    public bool PushCharacter(Vector3 attackerPosition, float strengh = 1, bool relativeToAttackerPosition = false)
    {
        if (!abilityManager.UsingAbility)
        {
            abilityManager.StopDetectHit();
            Vector3 pushVector = (transform.position - attackerPosition);
            pushVector.y = 0;
            pushVector = pushVector.normalized;
            //push distance depends on the player's position
            if (relativeToAttackerPosition)
            {
                var nearestColliderPointToAttacker = transform.position - pushVector * realRadius;
                strengh -= Vector3.Distance(nearestColliderPointToAttacker, attackerPosition);
            }
            animator.SetTrigger("Hit");
            pushVector *= strengh;
            return PerformPushing(pushVector);
        }
        else
        {
            animator.SetTrigger("HitInPlace");
        }

        return false;



    }
    protected abstract bool PerformPushing(Vector3 pushVector);
    //Main move character method
    public abstract void Move(Vector3 vector);
    
    protected virtual void Start()
    {
        
        
    }

   
}