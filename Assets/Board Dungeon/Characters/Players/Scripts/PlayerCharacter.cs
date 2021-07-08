using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerCharacter : Character
    {
        #region Movement Player Properties
        //Movement Player properties
        [SerializeField] private float movingTurnSpeed = 360;
        [SerializeField] private float stationaryTurnSpeed = 180;
        [SerializeField] private float moveSpeedMultiplier = 1f;
        [SerializeField] private float speedAnimationMultiplier = 1f;
        [SerializeField] private float runAmountScaler = 3.78f;
        [SerializeField] private float walkAmount = 1.58f;
        private bool m_IsGrounded;

        private float turnAmount;
        private float forwardAmount;
        private bool rotationEnabled = true;

        protected Rigidbody _rigidbody;
        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }


        public bool RotationEnabled { get => rotationEnabled; set => rotationEnabled = value; }
        public float MovingTurnSpeed { get => movingTurnSpeed; set => movingTurnSpeed = value; }
        public float TurnAmount { get => turnAmount; set => turnAmount = value; }
    #endregion



    protected override void MyAwake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

        // Start is called before the first frame update
        void Start()
        {
        
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        }
        // Update is called once per frame
        void Update()
        {

        }
        //Move player method
        public void Move(Vector3 move)
        {
            if (move.magnitude > 1f) move.Normalize(); //zostawia kierunek ustawia dlugosc na jeden
            move = transform.InverseTransformDirection(move); //zmienia kierunek z globalnego na lokalny
            move = Vector3.ProjectOnPlane(move, Vector3.up);
            turnAmount = Mathf.Atan2(move.x, move.z);
            forwardAmount = move.z;

            ApplyRotatation();
            UpdateAnimator(move);
        }

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
            // update the animator parameters
            float forwardAmountScaled = forwardAmount * runAmountScaler;

            if (forwardAmountScaled != 0 && forwardAmountScaled < walkAmount)
            {
                forwardAmountScaled = walkAmount;
            }


            animator.SetFloat("Speed", forwardAmountScaled, 0.4f, Time.deltaTime);
            animator.SetFloat("AngularSpeed", turnAmount, 0.2f, Time.deltaTime);
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


    }

