using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector, bool radians = false)
    {
        //Jezeli vector toVector jest 0 zeby nie bylo bledu
        if (toVector == Vector3.zero)
            return 0f;
        fromVector.y = 0;
        toVector.y = 0;

        float angle = Vector3.Angle(fromVector, toVector); //kat miedzy wektorami
        Vector3 normal = Vector3.Cross(fromVector, toVector);// zwraca wektor miedzy dwoma wektorami
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector)); //math.sign zwraca znak  // iloczyn skalarny

        if(radians)
        angle *= Mathf.Deg2Rad; //stopnie na radiany
                                // angle *= 0.425f;
        return angle;

    }
}
