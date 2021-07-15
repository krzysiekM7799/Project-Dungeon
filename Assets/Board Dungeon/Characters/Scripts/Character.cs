using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Basic components
    protected Animator animator;
    //Character properties
    protected CapsuleCollider m_Capsule;
    protected AbilityManager abilityManager;


    //Movement properties
    public float MaxSpeed { get; set; }
    public float CurrentSpeed { get; set; }
    protected bool rotationEnabled = true;
    public bool RotationEnabled { get => rotationEnabled; set => rotationEnabled = value; }


    //Targeting properties
    public Animator Animator { get => animator; set => animator = value; }
    public AbilityManager AbilityManager { get => abilityManager; set => abilityManager = value; }


    protected bool characterCanBePushed = true;
    protected float realRadius;

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


    public bool PushCharacter(Vector3 attackerPosition, float strengh = 1, bool relativeToAttackerPosition = false)
    {
        if (characterCanBePushed)
        {
            Vector3 pushVector = (transform.position - attackerPosition);
            pushVector.y = 0;
            pushVector = pushVector.normalized;
            //push distance depends on the player's position
            if (relativeToAttackerPosition)
            {
                var nearestColliderPointToAttacker = transform.position - pushVector * realRadius;
                strengh -= Vector3.Distance(nearestColliderPointToAttacker, attackerPosition);
            }
            Debug.Log(strengh);
            animator.SetTrigger("Hit");
            pushVector *= strengh;
            return PerformPushing(pushVector);
        }

        return false;



    }
    protected abstract bool PerformPushing(Vector3 pushVector);
    
    public abstract void Move(Vector3 vector);
    


    public void SetAnimatorParametr(AnimatorParametrType animatorParametrType, string parametrName, float parametrValue = 0)
    {
        switch (animatorParametrType)
        {
            case AnimatorParametrType.Trigger:
                animator.SetTrigger(parametrName);
                break;
            case AnimatorParametrType.Float:
                animator.SetFloat(parametrName, parametrValue);
                break;
            case AnimatorParametrType.Int:
                animator.SetInteger(parametrName, (int)parametrValue);
                break;
        }
    }

    // protected abstract void MyAwake();


    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}