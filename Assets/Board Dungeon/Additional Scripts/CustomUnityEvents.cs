using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomUnityEvents : MonoBehaviour
{
    public class FloatUnityEvent : UnityEvent<float> { }
    public class Float2UnityEvent : UnityEvent<float,float> { }
    public class Float3UnityEvent : UnityEvent<float,float,float> { }
    public class IntUnityEvent : UnityEvent<int> { }
    public class Int2UnityEvent : UnityEvent<int> { }
    public class Int3UnityEvent : UnityEvent<int> { }



}
