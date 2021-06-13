using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComboBehaviour : StateMachineBehaviour
{
    //Individual delay time to perform a combo 
    [SerializeField] private float maxComboDelay = 0.8f;
    [SerializeField] private int attackIndex;
    private Transform player;
    private PlayerCharacter playerCharacter;
    private ComboManager comboManager;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
        player = GameController.instance.Player;
        playerCharacter = player.GetComponent<PlayerCharacter>();
        comboManager = player.GetComponent<ComboManager>();
        comboManager.TransitionEnded = false;
        comboManager.Attacking = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Defining the moment at which triggering an attack is to be detected
        if (Time.time - comboManager.StartTime < maxComboDelay && comboManager.TransitionEnded)
        {
            comboManager.CanReceiveInput = true;
        }
        //Start next combo attac
        if (comboManager.InputReceived)
        {

            animator.SetBool("Attack", true);
            comboManager.MarkAttackAsUsed();
            comboManager.InputReceived = false;
        }
        playerCharacter.RotationEnabled = false;
        if (comboManager.Attacking)
        {
            RotateToTarget();
        }

    }
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //The time to perform a combo is counted from exit state time
        SetOnExitComboValues();
    }

    private void SetOnExitComboValues()
    {
        comboManager.TransitionEnded = true;
        comboManager.StartTime = Time.time;
        comboManager.Attacking = false;
        playerCharacter.RotationEnabled = true;
    }
    private void RotateToTarget()
    {
        Vector3 positionLook = comboManager.CurrentTarget.position - player.position;
        positionLook.y = 0;
        Quaternion rotation = Quaternion.LookRotation(positionLook);
        player.rotation = Quaternion.Slerp(player.rotation, rotation, Time.deltaTime * 5);

    }
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
