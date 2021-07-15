using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHelper : AnimatorHelper
{
    
    private Rigidbody rigidBody;

    public override void OnAnimatorMove()
    {
        {
            if (Time.deltaTime > 0)
            {
                Vector3 v = (animator.deltaPosition) / Time.deltaTime;


                v.y = rigidBody.velocity.y;
                rigidBody.velocity = v;


            //    agent.velocity = animator.deltaPosition / Time.deltaTime;
             //   transform.rotation = animator.rootRotation;

            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
       
        rigidBody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
