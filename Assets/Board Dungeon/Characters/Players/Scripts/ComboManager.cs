using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private bool canReceiveInput;
    private bool inputReceived;
    private float startTime;
    private bool transitionEnded;
    private bool attacking;
    private int currentAttackIndex;
    private PlayerCharacter playerCharacter;
    
    public bool CanReceiveInput { get => canReceiveInput; set => canReceiveInput = value; }
    public bool InputReceived { get => inputReceived; set => inputReceived = value; }
    public float StartTime { get => startTime; set => startTime = value; }
    public bool TransitionEnded { get => transitionEnded; set => transitionEnded = value; }
    public bool Attacking { get => attacking; set => attacking = value; }
    public int CurrentAttackIndex { get => currentAttackIndex; set => currentAttackIndex = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
    }
    public void MarkAttackAsUsed()
    {
       CanReceiveInput = !CanReceiveInput;
    }
    public void HandleAttack()
    {
        if (CanReceiveInput)
        {
            InputReceived = true;
            CanReceiveInput = false;

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            HandleAttack();


        }
    }
}
