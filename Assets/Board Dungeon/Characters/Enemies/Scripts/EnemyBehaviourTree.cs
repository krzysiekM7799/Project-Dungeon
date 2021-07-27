using System.Collections.Generic;
using UnityEngine;

//The behavior tree is constructed in this way for this time, in the future it will probably be based on xNode or another graphical interface cooperating with Unity
//You can choose which behaviour enemy needs, and add new by inheriting from this class
public class EnemyBehaviourTree : MonoBehaviour
{
    //Bool list of basic avaible behaviours, determines which behaviors will be available to AI
    [EnumNamedArray(typeof(BehaviourType))]
    [SerializeField] protected bool[] availableBehaviours = new bool[System.Enum.GetNames(typeof(BehaviourType)).Length];
    
    //Just root node of the behaviour tree, here calculating starts
    protected Selector rootNode;

    //Time between consecutive tree calculations
    [SerializeField] private float treeRecalculationFrequency = 0.01f;

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

    //Basic components needed by the class

    protected EnemyCharacter enemyCharacter;
    protected Transform playerTransform;
    protected PlayerCharacter playerCharacter;
   
    //Properties

    protected bool IsTreeStopped { get => isTreeStopped; set => isTreeStopped = value; }

    protected virtual void Awake()
    {
        enemyCharacter = GetComponent<EnemyCharacter>();
    }

    void Start()
    {
        playerTransform = GameController.instance.PlayerTransform;
        playerCharacter = GameController.instance.PlayerCharacter;

        ///So far the enemy escapes to the start of the dungeon
        triggeRunAwayNode = new MoveToTargetNode(enemyCharacter, Vector3.zero);
        
        MakeBehaviourTree();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculating tree is stopped when enemy is using ability
        if(!enemyCharacter.EnemyAbilityManager.UsingAbility)
       rootNode.Evaluate();
    }

    //The method creates the main tree of the opponent's behavior
    protected void MakeBehaviourTree()
    {
        List<BTNode> rootNodeChildren = new List<BTNode>();
       
        //First node to override, in nodes of this type we send logic method to delegate of node
        ActionNode firstNode = new ActionNode(FirstNode);
        rootNodeChildren.Add(firstNode);

        //Check if behaviour is needed, then add it to root
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
        
        ActionNode fifthNode = new ActionNode(FifthNode);
        rootNodeChildren.Add(fourthNode);

        rootNode = new Selector(rootNodeChildren);
    }

    private void MakeFollowPlayerNode(List<BTNode> rootNodeChildren)
    {
        MoveToTargetNode followPlayerNode = new MoveToTargetNode(enemyCharacter, playerTransform);
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




    //Methods can be overridden by simple methods, or subtrees created in child classes can be run in them, they are beetwen main nodes
    protected virtual NodeStates FirstNode()
    {
        return NodeStates.FAILURE;
    }

    protected virtual NodeStates SecondNode()
    {
        return NodeStates.FAILURE;
    }
    protected virtual NodeStates ThirdNode()
    {
        return NodeStates.FAILURE;
    }
    protected virtual NodeStates FourthNode()
    {

        return NodeStates.FAILURE;
    }
    protected NodeStates FifthNode()
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
    
    //Node to trigge dodge
    protected virtual NodeStates TriggeDodge()
    {
        enemyCharacter.EnemyAbilityManager.UsingAbility = true;
        enemyCharacter.Animator.SetTrigger("Dodge");
                
        return NodeStates.SUCCESS;
    }

   
}
