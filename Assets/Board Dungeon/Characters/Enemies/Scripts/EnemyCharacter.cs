using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : Character
{
    //Animation properties
    [SerializeField] protected float speedDampTime;
    [SerializeField] protected float angularSpeedDumpTime;
    [SerializeField] protected float angleResponseTime;

    [SerializeField] protected float stopDistance;
    [SerializeField] protected float maxSpeed = 1;
    [SerializeField] protected float angleToTurnWhenMove = 0.6f;
    private Transform player;
    private bool cooldownAttack = false;

    public NavMeshAgent agent;
    
    private Vector3 wherePush;
    private float lastTimePushed;
    private bool isPushed;






    
    public override void Move(Vector3 where)
    {
        float currentSpeed;


        float angleGreaterThan = angleToTurnWhenMove;
        //When is near to point
        if (Vector3.Distance(transform.position, where) < 2.5f)
        {
            currentSpeed = 0f;
            angleGreaterThan = 0.3f;
            
            
        }
        else
        {
            currentSpeed = maxSpeed;
        }
        
        float angle = ThingCalculator.FindAngle(transform.forward, where - transform.position, transform.up, true);
       
        
        //Debug.Log(angle);
        var animatorSpeed = animator.GetFloat("Speed");
        
        if ( Mathf.Abs(angle)  > angleGreaterThan && animatorSpeed < maxSpeed * 0.7f)
        {
            animator.SetBool("Turning", true);
            UpdateAnimator(0f, angle);
            
        }
        else
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
    public void Turn()
    {

    }

    protected void UpdateAnimator(float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;
        animator.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        animator.SetFloat("AngularSpeed", angularSpeed, angularSpeedDumpTime, Time.deltaTime );
        
    }
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

    protected void PushAgentLerp()
    {
        if (isPushed && Time.time - lastTimePushed < 0.4f)
        {


            float speed = Time.deltaTime * 5;

            Vector3 lerp = Vector3.Lerp(wherePush, Vector3.zero, speed);



            agent.velocity = lerp;
        }
        else if (isPushed)
        {

            isPushed = false;
            agent.velocity = Vector3.zero;

        }
    }

    protected override void Awake()
    {
        base.Awake();
       
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameController.instance.Player;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Move(player.position);
    }
    private void LateUpdate()
    {
        PushAgentLerp(); 
    }

}
