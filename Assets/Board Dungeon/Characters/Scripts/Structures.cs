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
    public ParentOfObject parentOfColldier;
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
public struct CurrentAbilityProperties
{
    public int currentAbilityIndex;
    public AbilityType currentAbilityType;
    public ColliderType currentColliderType;
    public ParentOfObject currentParentOfCollider;
    public int attackDmg;
    public float attackDmgMultiplier;
    public int abilityPower;
    public float abilityPowerMultiplier;
    public float strenghOfPush;
    public bool isAutoTarget;
    public float minDistanceToUse;
    public AbilityEffect abilityEffect;
    public float abilityEffectValue;
    public float abilityEffectTime;
}
[System.Serializable]
public struct PlayerAbilityProperties
{
    [SerializeField] private Sprite image;
    public Sprite Image { get => image;}
    public CustomUnityEvents.FloatUnityEvent OnAbilityUse;
}
