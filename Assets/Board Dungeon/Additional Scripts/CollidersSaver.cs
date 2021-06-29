using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersSaver : MonoBehaviour
{
    public BasicAttack basicAttack;
    public Transform attackCollider;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void SetAttackColliderProperties()
    {
        basicAttack._attackColliderProperties.positionOfCollider = attackCollider.localPosition;
        basicAttack._attackColliderProperties.rotationOfCollider = attackCollider.localRotation;

        BoxCollider boxCollider = attackCollider.GetComponent<BoxCollider>();
        SphereCollider sphereCollider = attackCollider.GetComponent<SphereCollider>();
        if (boxCollider != null && boxCollider.enabled)
        {
            basicAttack._attackColliderProperties.centerOfCollider = boxCollider.center;
            basicAttack._attackColliderProperties.sizeOfCollider = boxCollider.size;
        }
        else if (sphereCollider != null && sphereCollider.enabled)
        {
            basicAttack._attackColliderProperties.centerOfCollider = sphereCollider.center;
            basicAttack._attackColliderProperties.radiusOfCollider = sphereCollider.radius;
        }
        else
        {
            Debug.LogError("You didnt set collider properties in ScriptableObject of Ability");
        }
    }
    // Update is called once per frame
}
