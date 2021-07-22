using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacter : Character
{
    #region Movement Player Properties
    //Movement Player properties
    [Header("Movement Player properties")]
    [SerializeField] private float movingTurnSpeed = 360;
    [SerializeField] private float stationaryTurnSpeed = 180;
    [SerializeField] private float moveSpeedMultiplier = 1f;
    [SerializeField] private float speedAnimationMultiplier = 1f;
    [SerializeField] private float runAmountScaler = 3.78f;
    [SerializeField] private float walkAmount = 1.58f;
    [SerializeField] private float speedDamp = 0.4f;
    [SerializeField] private float angularSpeedDamp = 0.2f;
    private float turnAmount;
    private float forwardAmount;

    //Basic player components
    private PlayerStats playerStats;
    private PlayerAbilityManager playerAbilityManager;
    private ComboManager comboManager;
    private Rigidbody _rigidbody;
    public PlayerStats PlayerStats { get => playerStats; set => playerStats = value; }
    public PlayerAbilityManager PlayerAbilityManager { get => playerAbilityManager; set => playerAbilityManager = value; }
    public ComboManager ComboManager { get => comboManager; set => comboManager = value; }
    public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        playerStats = GetComponent<PlayerStats>();
        playerAbilityManager = GetComponent<PlayerAbilityManager>();
        comboManager = GetComponent<ComboManager>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    }

    //Move player method
    public override void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize(); //zostawia kierunek ustawia dlugosc na jeden
        move = transform.InverseTransformDirection(move); //zmienia kierunek z globalnego na lokalny
        move = Vector3.ProjectOnPlane(move, Vector3.up);
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;

        ApplyRotatation();
        UpdateAnimator(move);
    }
    //Dash method will be improved
    public void MakeDash()
    {
        animator.SetTrigger("Dash");
    }

    //Method which is helping to rotate player GO
    void ApplyRotatation()
    {
        if (rotationEnabled)
        {
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }
    }

    void UpdateAnimator(Vector3 move)
    {
        float forwardAmountScaled = forwardAmount * runAmountScaler;

        if (forwardAmountScaled != 0 && forwardAmountScaled < walkAmount)
        {
            forwardAmountScaled = walkAmount;
        }
        animator.SetFloat("Speed", forwardAmountScaled, speedDamp, Time.deltaTime);
        animator.SetFloat("AngularSpeed", turnAmount, angularSpeedDamp, Time.deltaTime);
    }

    IEnumerator DashRootationSpeedUp(float duration)
    {
        float _movingTurnSpeed = movingTurnSpeed;
        movingTurnSpeed *= 3;
        yield return new WaitForSeconds(duration);
        movingTurnSpeed = _movingTurnSpeed;
    }

    public void SetDashRotationSpeedUp(float duration)
    {
        StartCoroutine(DashRootationSpeedUp(duration));
    }

    protected override bool PerformPushing(Vector3 pushVector)
    {
        return true;
    }
}

