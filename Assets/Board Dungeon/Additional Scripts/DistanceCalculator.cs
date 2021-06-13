using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    [SerializeField]public Transform point1;
    [SerializeField] public Transform point2;
    [SerializeField] public float result;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CalculateDistance()
    {
        result = Vector3.Distance(point1.position, point2.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
