using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorHelper : AnimatorHelper
{
    private UnityEngine.AI.NavMeshAgent agent;

    public override void OnAnimatorMove()
    {
        {
            if (Time.deltaTime > 0)
            {
                Vector3 v = (animator.deltaPosition) / Time.deltaTime;


               


                    agent.velocity = animator.deltaPosition / Time.deltaTime;
                   transform.parent.rotation = animator.rootRotation;

            }
        }
    }

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();

    }
}
