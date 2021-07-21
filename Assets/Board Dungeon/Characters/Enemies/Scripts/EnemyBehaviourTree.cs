using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    //Bool list of basic avaible behaviours, determines which behaviors will be available to AI
    [EnumNamedArray(typeof(BehaviourType))]
    [SerializeField] protected bool[] availableBehaviours = new bool[System.Enum.GetNames(typeof(BehaviourType)).Length];
   
    
    //Just root node of the behaviour tree
    protected Selector rootNode;

   
 
    //Basic components needed by the class
    protected EnemyCharacter enemyCharacter;
    protected Transform playerTransform;
    protected PlayerCharacter playerCharacter;

    [SerializeField] private float treeRecalculationFrequency = 0.01f;
    private ElapsedTimeChecker elapsedTimeChecker;
    //Variable specifying the run state of the tree
    protected bool isTreeStopped = false;
    [Header("RunAwayNode properties")]
    [Range(0f,0.9f)][Tooltip("The percentage below which health points must drop for the opponent to run away")]
    [SerializeField] int hpPercentToRunAway = 30;
    MoveToTargetNode triggeRunAwayNode;
    
    [Header("DodgeNode properties")]
    [Tooltip("Specifies the percentage chance to dodge.")]
    [SerializeField] int percentChanceToDodge = 10;
    [Tooltip("Defines the time interval at which the opponent can try to dodge")]
    [SerializeField] private float timeBeetwenDodgeTries = 2;

    //Properties
    protected bool IsTreeStopped { get => isTreeStopped; set => isTreeStopped = value; }

    protected virtual void Awake()
    {
        enemyCharacter = GetComponent<EnemyCharacter>();

    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameController.instance.PlayerTransform;
        playerCharacter = GameController.instance.PlayerCharacter;

        triggeRunAwayNode = new MoveToTargetNode(enemyCharacter, Vector3.zero);
        
        MakeBehaviourTree();
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemyCharacter.EnemyAbilityManager.UsingAbility)
       rootNode.Evaluate();
    }
 
    protected void MakeBehaviourTree()
    {
        List<BTNode> rootNodeChildren = new List<BTNode>();
        //First node to override;
        ActionNode firstNode = new ActionNode(FirstNode);
        rootNodeChildren.Add(firstNode);

        if (availableBehaviours[(int)BehaviourType.RunningAway])
        {
            MakeRunAwayNode(rootNodeChildren);
        }
       
        //Second node to override;
        ActionNode secondNode = new ActionNode(SecondNode);
        rootNodeChildren.Add(secondNode);

        if (availableBehaviours[(int)BehaviourType.Dodging])
        {
            MakeDodgeNode(rootNodeChildren);
        }

        //Third node to override;
        ActionNode thirdNode = new ActionNode(ThirdNode);
        rootNodeChildren.Add(thirdNode);

        if (availableBehaviours[(int)BehaviourType.UsingAbilities])
        {
            MakeAbilitiesNode(rootNodeChildren);

        }
        //Fourth node to override;
        ActionNode fourthNode = new ActionNode(FourthNode);
        rootNodeChildren.Add(fourthNode);

        if (availableBehaviours[(int)BehaviourType.FollowingPlayer])
        {
            MakeFollowPlayerNode(rootNodeChildren);
        }

        rootNode = new Selector(rootNodeChildren);
    }

    private void MakeFollowPlayerNode(List<BTNode> rootNodeChildren)
    {
        MoveToTargetNode followPlayerNode = new MoveToTargetNode(enemyCharacter, enemyCharacter.PlayerTransform);
        rootNodeChildren.Add(followPlayerNode);
    }

    private void MakeAbilitiesNode(List<BTNode> rootNodeChildren)
    {
        List<BTNode> abilitiesNodeChildren = new List<BTNode>();

        for (int i = 0; i < enemyCharacter.EnemyAbilityManager.AbilitiesCount; i++)
        {
            ActionNode useAbility = new ActionNode(enemyCharacter.EnemyAbilityManager.EnemyAbilities[i].UseAbilityForNode);
            abilitiesNodeChildren.Add(useAbility);
        }



        Selector abilitiesNode = new Selector(abilitiesNodeChildren);

        rootNodeChildren.Add(abilitiesNode);
    }

    protected virtual NodeStates FourthNode()
    {
        
        return NodeStates.FAILURE;
    }

    protected virtual NodeStates ThirdNode()
    {

        return NodeStates.FAILURE;
    }

 
    private void MakeRunAwayNode(List<BTNode> rootNodeChildren)
    {
        Sequence runAwayNode = new Sequence();
        List<BTNode> runAwayChildren = new List<BTNode>();

        if (hpPercentToRunAway != 0)
            runAwayChildren.Add(new CheckHpNode(hpPercentToRunAway, enemyCharacter.EnemyStats));


        runAwayNode.SetChildrenOfNode(runAwayChildren);
        runAwayNode.SetChildrenOfNode(Sequence.MakeSimpleSequenceNode(CheckRunAwayConditions, TriggeRunAway));

        rootNodeChildren.Add(runAwayNode);
    }
    private void MakeDodgeNode(List<BTNode> rootNodeChildren)
    {
        Sequence dodgeNode = new Sequence();
        List<BTNode> dodgeChildren = new List<BTNode>();
        if (percentChanceToDodge != 0)
        {
            dodgeChildren.Add(new CheckDodgeNode(enemyCharacter, playerCharacter.PlayerAbilityManager, playerCharacter.ComboManager, percentChanceToDodge, timeBeetwenDodgeTries));
        }
        dodgeNode.SetChildrenOfNode(dodgeChildren);
        dodgeNode.SetChildrenOfNode(Sequence.MakeSimpleSequenceNode(CheckDodgeConditions, TriggeDodge));
        rootNodeChildren.Add(dodgeNode);
    }




    //Methods can be overridden by simple methods, or subtrees created in child classes can be run in them
    protected virtual NodeStates FirstNode()
    {


        return NodeStates.FAILURE;

    }
    protected virtual NodeStates SecondNode()
    {

        return NodeStates.FAILURE;
    }

    protected virtual NodeStates CheckRunAwayConditions()
    {

        
            return NodeStates.SUCCESS;

    }
    protected virtual NodeStates TriggeRunAway()
    {
        triggeRunAwayNode.Evaluate();

        return NodeStates.SUCCESS;
    }
    protected virtual NodeStates CheckDodgeConditions()
    {
       
        return NodeStates.SUCCESS;

        

        
    }

    protected virtual NodeStates TriggeDodge()
    {
        enemyCharacter.EnemyAbilityManager.UsingAbility = true;
        enemyCharacter.Animator.SetTrigger("Dodge");
                
        return NodeStates.SUCCESS;
    }

   
}
