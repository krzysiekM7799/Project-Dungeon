using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Basic components
    protected Animator animator;
    //Character properties
    protected CapsuleCollider m_Capsule;
    protected AbilityManager abilityManager;


    //Movement properties
    public float MaxSpeed { get; set; }
    public float CurrentSpeed { get; set; }

   
    //Targeting properties
    public Animator Animator { get => animator; set => animator = value; }
    public AbilityManager AbilityManager { get => abilityManager; set => abilityManager = value; }

    


    public float GetRealRadiusRadius()
    {
        return m_Capsule.radius * transform.localScale.x;
    }
 
    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        m_Capsule = GetComponent<CapsuleCollider>();
        abilityManager = GetComponent<AbilityManager>();
        MyAwake();
    }

    protected virtual void MyAwake()
    {

    }
   

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
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}