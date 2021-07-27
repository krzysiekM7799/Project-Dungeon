using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Additional class
public class ThingCalculator : MonoBehaviour
{
    //Method for finding an angle with a side specification
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

    //Abilities do not have to have specific values for each level of abilities, if there are fewer of them, and the level of abilities is higher, then the highest value is returned
    public static int CheckAbilityLvl(int length, int abilityLvl)
    {
        if (length - 1 < abilityLvl)
        {
            return length - 1;
        }

        return abilityLvl;
    }

    //Just randomize chance of doing something
    public static bool TryWithPercentChance(int percentChance)
    {
        var random = Random.Range(0, 101);
        if (random <= percentChance)
        {
            return true;
        }
       
        return false;
    }

    public static int ClampPositive(int value)
    {
        if (value <= 0)
        {
            return 0;
        }
        else
        {
            return value;
        }
    }

    public static float ClampPositive(float value)
    {
        if (value <= 0f)
        {
            return 0f;
        }
        else
        {
            return value;
        }
    }
}
