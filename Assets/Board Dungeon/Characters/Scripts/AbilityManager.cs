using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public BoxCollider attackBoxCollider;
    public SphereCollider attackSphereColldier;
    AbilityType currentAbilityType = AbilityType.BasicAttack;
    [SerializeField] BasicAttackColliderProperties basicAttackColliderProperties;
    private BasicAttackColliderProperties BasicAttackColliderProperties1 { get => basicAttackColliderProperties; set => basicAttackColliderProperties = value; }

    [System.Serializable]
    struct BasicAttackColliderProperties
    {
        public Vector3 positionOfCollider;
        public Vector3 centerOfCollider;
        public Vector3 sizeOfCollider;
        public Quaternion rotationOfCollider;
    }

    //Methods for enable or disable attack collider for animation events
    public void SetAttackColliderOn(int typeOfCollider)
    {
        switch (typeOfCollider)
        {
            case 0:
                attackBoxCollider.enabled = true;
                break;
            case 1:
              attackSphereColldier.enabled = true;
                break;
          
        }
    }
    public void SetAttackColliderOff(int typeOfCollider)
    {
        switch (typeOfCollider)
        {
            case 0:
                attackBoxCollider.enabled = false;
                break;
            case 1:
                attackSphereColldier.enabled = false;
                break;

        }
    }
    public void UseBasicAttack()
    {
        currentAbilityType = AbilityType.BasicAttack;
        attackBoxCollider.transform.localPosition = basicAttackColliderProperties.positionOfCollider;
        attackBoxCollider.transform.localRotation = basicAttackColliderProperties.rotationOfCollider;
        attackBoxCollider.size = basicAttackColliderProperties.sizeOfCollider;
        attackBoxCollider.center = basicAttackColliderProperties.centerOfCollider;
        
         
    }
    public void UseAttackAbility()
    {
        currentAbilityType = AbilityType.AttackAbility;
        
        
    }
    private void Awake()
    {
        SetPropertiesOfColliderBasicAttack();
    }
    // Basic attack collider properties are downloaded from the scene before start, you have to set it on scene for each character
    private void SetPropertiesOfColliderBasicAttack()
    {
        basicAttackColliderProperties.positionOfCollider = attackBoxCollider.transform.localPosition;
        basicAttackColliderProperties.rotationOfCollider = attackBoxCollider.transform.localRotation;
        basicAttackColliderProperties.sizeOfCollider = attackBoxCollider.size;
        basicAttackColliderProperties.centerOfCollider = attackBoxCollider.center;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum AbilityType
    {
        BasicAttack,
        AttackAbility,
        BuffAbility,
    }


}
