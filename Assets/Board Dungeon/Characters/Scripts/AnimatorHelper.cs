using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelper : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    private Animator animator;
    private Rigidbody rigidBody;
    private AbilityManager abilityManager;
    private Stats myStats;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponentInParent<PlayerCharacter>();
        abilityManager = GetComponentInParent<AbilityManager>();
        animator = playerCharacter.Animator;
        rigidBody = playerCharacter.Rigidbody;
        myStats = GetComponentInParent<Stats>();
    }



    public void UseBasicAttack()
    {
        abilityManager.UseBasicAttack();
    }
    public void SetAttackColliderOn(int typeOfCollider)
    {
        abilityManager.SetAttackColliderOn(typeOfCollider);
    }
    public void SetAttackColliderOff(int typeOfCollider)
    {
        abilityManager.SetAttackColliderOff(typeOfCollider);
    }
    public void SetPushDistance(float pushDistance)
    {
       myStats.SetPushDistance(pushDistance);
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimatorMove()
    {
        if (Time.deltaTime > 0)
        {
            Vector3 v = (animator.deltaPosition) / Time.deltaTime;


            v.y = rigidBody.velocity.y;
            rigidBody.velocity = v;
        }
    }
}