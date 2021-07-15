using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    protected bool isTreeStopped = false;
    [SerializeField] protected bool[] availableBehaviours = new bool[System.Enum.GetNames(typeof(BehaviourType)).Length];
    protected Selector rootNode;
   
    
    protected Sequence abilitiesWhileMovingNode;
    protected Sequence followPlayerNode;
    protected Sequence patrolNode;
    protected AbilityManager abilityManager;
    

    protected bool IsTreeStopped { get => isTreeStopped; set => isTreeStopped = value; }



    protected void MakeBehaviourTree()
    {
        List<BTNode> rootNodeChildren = new List<BTNode>();

        if (availableBehaviours[(int)BehaviourType.RunningAway])
        {
            Sequence runAwayNode;
            runAwayNode = new Sequence();
            runAwayNode.SetChildrenOfNode(runAwayNode.MakeSimpleSequenceNode(CheckRunAwayConditions, TriggeRunAway));
            rootNodeChildren.Add(runAwayNode);
        }
        if (availableBehaviours[(int)BehaviourType.Dodging])
        {
            Sequence dodgeNode;
            dodgeNode = new Sequence();
            dodgeNode.SetChildrenOfNode(dodgeNode.MakeSimpleSequenceNode(CheckDodgeConditions, TriggeDodge));
            rootNodeChildren.Add(dodgeNode);
        }
       /* if (availableBehaviours[(int)BehaviourType.UsingAbilities])
        {
            List<BTNode> abilitiesNodeChildren = new List<BTNode>();
          
            for (int i = 0; i < abilityManager.AbilitiesCount; i++)
            {
                ActionNode useAbility = new ActionNode(UseAbility, i);
                abilitiesNodeChildren.Add(useAbility);
            }

           

            Selector abilitiesNode = new Selector(abilitiesNodeChildren);

          //  rootNodeChildren.Add(abilitiesNode);
        
        }*/












        /*  List<BTNode> attackChildren = new List<BTNode>();

          attackChildren.Add(isAttackNotCooldownedNode);
          attackChildren.Add(isDistanceEnoughAttackNode);
          attackChildren.Add(isAngleEnoughToAttackNode);
          attackChildren.Add(triggeAttackNode);
          if (attackNodeList.Count > 0)
          {
              foreach (BTNode node in attackNodeList)
              {
                  attackChildren.Add(node);
              }
          }
          else
          {
              Debug.Log("attackNodeList is empty");
          }
          attackNode = new Sequence(attackChildren);


          */
      

       

        
        
     
       
        rootNode = new Selector(rootNodeChildren);
    }


    protected virtual NodeStates UseAbility(int abilityID)
    {

       if(abilityManager.abilities[abilityID].TriggeAbility())
        return NodeStates.SUCCESS;

       return NodeStates.FAILURE;
    }


    protected virtual NodeStates CheckDodgeConditions()
    {
        return NodeStates.SUCCESS;
    }

    protected virtual NodeStates CheckRunAwayConditions()
    {

        if (Vector3.Distance(GameController.instance.Player.position, transform.position) > 2f)
            return NodeStates.SUCCESS;

        return NodeStates.FAILURE;
  


    }
    protected virtual NodeStates TriggeRunAway()
    {
        Debug.Log("runaway");
        return NodeStates.SUCCESS;
    }
    protected virtual NodeStates TriggeDodge()
    {
        Debug.Log("dodge");
        return NodeStates.SUCCESS;
    }


    // Start is called before the first frame update
    void Start()
    {
        MakeBehaviourTree();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rootNode.Evaluate();
    }
}
