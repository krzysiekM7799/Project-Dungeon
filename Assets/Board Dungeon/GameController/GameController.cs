using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;
    void Awake()
    {
        
        playerTransform = FindObjectOfType<PlayerCharacter>().transform;
        playerCharacter = playerTransform.GetComponent<PlayerCharacter>();
        instance = this;
    }

    #endregion
    Transform playerTransform;
    PlayerCharacter playerCharacter;

    public Transform PlayerTransform { get => playerTransform; set => playerTransform = value; }
    public PlayerCharacter PlayerCharacter { get => playerCharacter; set => playerCharacter = value; }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
