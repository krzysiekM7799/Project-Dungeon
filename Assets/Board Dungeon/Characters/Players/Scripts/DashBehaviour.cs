using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : StateMachineBehaviour
{
    private Transform player;
    private PlayerCharacter playerCharacter;
    private PlayerUserControl playerUserControl;
    private ComboManager comboManager;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameController.instance.Player;
        if (playerCharacter == null)
        {
            playerCharacter = player.GetComponent<PlayerCharacter>();
            playerUserControl = player.GetComponent<PlayerUserControl>();
            comboManager = player.GetComponent<ComboManager>();

            Debug.Log("pobieram playera");
        }
        else
        {
            Debug.Log("nie pobieram playera");
        }
        comboManager.Attacking = false;
        playerCharacter.RotationEnabled = true;
        var joystickTranslate = playerUserControl.LeftJoystick.Vertical * playerUserControl.CameraForward + playerUserControl.LeftJoystick.Horizontal * playerUserControl.CameraRight;
        var angleBeetwenAnalogAndPlayer = ThingCalculator.FindAngle(player.forward, joystickTranslate, player.up);
        player.transform.Rotate(0, angleBeetwenAnalogAndPlayer, 0);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("Speed", 3.6f, 0.02f, Time.deltaTime);
        

       



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        
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
