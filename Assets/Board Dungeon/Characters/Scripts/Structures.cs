using UnityEngine;

[System.Serializable]
public struct AttackColliders
{
    public Transform attackColliderTransform;
    public BoxCollider attackBoxCollider;
    public SphereCollider attackSphereCollider;
}
[System.Serializable]
public struct AttackColliderProperties
{
    public ColliderType colliderType;
    public ParentOfCollider parentOfColldier;
    public Vector3 positionOfCollider;
    public Quaternion rotationOfCollider;
    public Vector3 centerOfCollider;
    [Tooltip("Set it if attack use BoxCollider")]
    public Vector3 sizeOfCollider;
    [Tooltip("Set it if attack use SphereCollider")]
    public float radiusOfCollider;

}
public struct BasicAttackProperties
{
    public int attackDmgModifier;
    public float strenghOfPush;
}
public struct AttackAbilityProperties
{
    public float strenghOfPush;
    public int attackDmg;
    public int abilityPower;
    public AbilityEffect abilityEffect;
    public float abilityEffectValue;
    public float abilityEffectTime;
}
public struct CurrentAbilityInformation
{
    public int currentAbilityIndex;
    public AbilityType currentAbilityType;
    public ColliderType currentColliderType;
    public ParentOfCollider currentParentOfCollider;
}
