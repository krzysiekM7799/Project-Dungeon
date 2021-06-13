using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Basic components
    protected Animator animator;
    //Character properties
    protected CapsuleCollider m_Capsule;
    protected Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }


    //Movement properties
    public float MaxSpeed { get; set; }
    public float CurrentSpeed { get; set; }

   
    //Targeting properties
    public float DistanceToCurrentTarget { get; set; }
    public Animator Animator { get => animator; set => animator = value; }

    public Transform CurrentTarget;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        //MyAwake();
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
