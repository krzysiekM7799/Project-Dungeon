using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingCalculator : MonoBehaviour
{
    public static float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector, bool radians = false)
    {
        //If toVector is 0 just return 0 to avoid errors
        if (toVector == Vector3.zero)
            return 0f;
        
        fromVector.y = 0;
        toVector.y = 0;

        //Counting angle
        float angle = Vector3.Angle(fromVector, toVector); //kat miedzy wektorami
        
        //Left hand rule, determine from which side the target is located 
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        
        //Determine the sign of the angle to distinguish between left and right
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector)); 

        //Mecanim animator needs value in radians
        if(radians)
        angle *= Mathf.Deg2Rad; 

        return angle;
    }
    public static int CheckAbilityLvl(int length, int abilityLvl)
    {
        if (length - 1 < abilityLvl)
        {
            return length - 1;
        }

        return abilityLvl;
    }
    public static bool TryWithPercentChance(int percentChance)
    {
        var random = Random.Range(0, 101);
        if (random <= percentChance)
        {
            return true;
        }
       
        return false;
    }
}
