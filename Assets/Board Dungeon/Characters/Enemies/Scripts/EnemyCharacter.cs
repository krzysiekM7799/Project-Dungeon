using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : Character
{
    //Movement Properties

    [Header("Movement animation properties")]
    [SerializeField] protected float speedDampTime;
    [SerializeField] protected float angularSpeedDumpTime;
    [SerializeField] protected float angleResponseTime;
    [Header("Movement properties")]
    [SerializeField] protected float stoppingDistance;
    [SerializeField] protected float maxSpeed = 1;
    [SerializeField] protected float angleGreaterThanToTurnInPlace = 0.6f;

    //Basic enemy components

    private EnemyStats enemyStats;
    private EnemyAbilityManager enemyAbilityManager;
    private NavMeshAgent agent;
    private Transform playerTransform;

    //Properties related to the pushing method

    private Vector3 wherePush;
    private float lastTimePushed;
    private bool isPushed;

    //Properties

    public EnemyStats EnemyStats { get => enemyStats; set => enemyStats = value; }
    public EnemyAbilityManager EnemyAbilityManager { get => enemyAbilityManager; set => enemyAbilityManager = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public Transform PlayerTransform { get => playerTransform; set => playerTransform = value; }

    protected override void Awake()
    {
        base.Awake();
        enemyStats = GetComponent<EnemyStats>();
        enemyAbilityManager = GetComponent<EnemyAbilityManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        base.Start();
        agent.updateRotation = false;
    }

    private void LateUpdate()
    {
        PushAgentLerp();
    }

    public override void Move(Vector3 where)
    {
        float currentSpeed;
        //Variable which control when enemy have to rotate in place (play rotate in place animation)
        float angleGreaterThan = angleGreaterThanToTurnInPlace;

        //When is near to point
        if (Vector3.Distance(transform.position, where) < stoppingDistance + realRadius)
        {
            currentSpeed = 0f;
            angleGreaterThan = 0.3f;
        }
        else //When is far to point
        {
            currentSpeed = maxSpeed;
        }

        //Counting angle to destination point 
        float angle = ThingCalculator.FindAngle(transform.forward, where - transform.position, transform.up, true);

        var animatorSpeed = animator.GetFloat("Speed");

        //Turning in place
        if (Mathf.Abs(angle) > angleGreaterThan && animatorSpeed < maxSpeed * 0.7f)
        {
            animator.SetBool("Turning", true);
            UpdateAnimator(0f, angle);
        }
        else //Moving forward with interpolated turning
        {
            animator.SetBool("Turning", false);
            UpdateAnimator(currentSpeed, 0);
            if (animatorSpeed > maxSpeed * 0.7f)
            {
                Vector3 positionLook = where - transform.position;
                positionLook.y = 0;
                Quaternion rotation = Quaternion.LookRotation(positionLook);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
            }
        }
    }

    //Here is updating basic animator values
    protected void UpdateAnimator(float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;
        animator.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        animator.SetFloat("AngularSpeed", angularSpeed, angularSpeedDumpTime, Time.deltaTime);

    }

    //Start pushing method
    protected override bool PerformPushing(Vector3 pushVector)
    {
        PushAgent(pushVector);
        return true;
    }

    public void PushAgent(Vector3 wherePush)
    {
        this.wherePush = wherePush;
        lastTimePushed = Time.time;

        isPushed = true;
    }

    //Pushing enemies character is done with the rigidbody velocity field for the duration of the animation being played
    protected void PushAgentLerp()
    {
        if (isPushed && Time.time - lastTimePushed < 0.4f)
        {
            float pushSpeed = Time.deltaTime * 5;
            Vector3 lerp = Vector3.Lerp(wherePush, Vector3.zero, pushSpeed);
            agent.velocity = lerp;
        }
        else if (isPushed)
        {
            isPushed = false;
            agent.velocity = Vector3.zero;
        }
    }


}
