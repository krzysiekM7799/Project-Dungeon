using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshBaker : MonoBehaviour
{

    [SerializeField]
    NavMeshSurface navMeshSurface;


    // Start is called before the first frame update
    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
       
        // Debug.Log("ZMESHOWALO");
    }

    // Update is called once per frame

}
