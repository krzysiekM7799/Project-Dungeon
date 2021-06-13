using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;
    void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerCharacter>().transform;
    }

    #endregion
    Transform player;

    public Transform Player { get => player; set => player = value; }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
