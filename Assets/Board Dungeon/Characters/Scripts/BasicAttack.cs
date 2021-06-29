using UnityEngine;

[CreateAssetMenu(fileName = "NewBasicAttack", menuName = "Abilities/BasicAttack")]
public class BasicAttack : ScriptableObject
{
    public int attackDmgModifier;
    public float strenghOfPush;
    public AttackColliderProperties _attackColliderProperties;


}