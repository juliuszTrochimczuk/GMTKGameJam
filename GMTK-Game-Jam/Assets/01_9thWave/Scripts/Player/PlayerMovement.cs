using System.Collections;
using _01_9thWave.Scripts.Audio;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;
using Random = UnityEngine.Random;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private UnityEvent onJump;
        [SerializeField] private UnityEvent onLanding;
        [SerializeField] private UnityEvent<float> onChangingDirection;

        [SerializeField] private LayerMask _groundLayers;

        [Header("Walking")]
        [SerializeField] private float _groundMaxSpeed;
        [SerializeField] private float _groundMoveSmoother;
        [SerializeField] private float _timeBetweenSteps;


        [Header("Jumping")]
        [SerializeField] private float _inAirMaxSpeed;
        [SerializeField] private float _inAirMoveSmoother;
        [SerializeField] private float maxTimeInAir;
        [SerializeField] private float _jumpForce;
        [SerializeField] private AnimationCurve _jumpCurve;
        
        private CircleCollider2D _collider;
        private Rigidbody2D _rb;

        private float _verticalVelocity;
        private float _horizontalVelocity;
        private float _currentHorizontalVelocity;

        private float _walkTimer;

        public float InputDirection { get; private set; }
        private float _MaxSpeed
        {
            get
            {
                if (_onGround)
                    return _groundMaxSpeed;
                else
                    return _inAirMaxSpeed;
            }
        }
        private float _MoveSmoother
        {
            get
            {
                if (_onGround)
                    return _groundMoveSmoother;
                else
                    return _inAirMoveSmoother;
            }
        }
        private bool _onGround;
        private bool _canMove = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
        }

        private void FixedUpdate()
        {
            IsPlayerOnGround();

            if (_onGround)
                _rb.gravityScale = 1;
            else
                _rb.gravityScale = 9.89f;

            _horizontalVelocity = Mathf.SmoothDamp(_horizontalVelocity, InputDirection, ref _currentHorizontalVelocity, _MoveSmoother);
            
            if (_canMove)
                _rb.velocity = new Vector2(_horizontalVelocity * _MaxSpeed, _verticalVelocity);
            
            _walkTimer += Time.fixedDeltaTime;

            if (_onGround && Mathf.Abs(InputDirection) > 0.01f)
            {
                if (_walkTimer >= _timeBetweenSteps)
                {
                    _walkTimer = Random.Range(0f, 0.2f);
                    AudioManager.Instance.PlayFootstepEffects();
                }
            }
        }

        public void ReadMoveInputVector(CallbackContext ctx)
        {
            InputDirection = ctx.ReadValue<float>();
            onChangingDirection.Invoke(InputDirection);
        }

        public void Jump(CallbackContext ctx)
        {
            if (_onGround && ctx.performed)
                StartCoroutine(Jumping());
        }

        public void Jump(float jumpForce, float inAirTime) => StartCoroutine(Jumping(jumpForce, inAirTime));

        public void StunPlayer(float delay) => StartCoroutine(MovementDelay(delay));
        
        private IEnumerator MovementDelay(float delay)
        {
            _canMove = false;
            _rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(delay);
            _canMove = true;
        }

        private IEnumerator Jumping()
        {
            float time = 0.0f;
            onJump.Invoke();
            do
            {
                _verticalVelocity = _jumpForce * _jumpCurve.Evaluate(time / maxTimeInAir);

                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            while (time <= maxTimeInAir || !_onGround);
            onLanding.Invoke();
            _verticalVelocity = 0;
        }

        private IEnumerator Jumping(float jumpForce, float inAirTime)
        {
            float time = 0.0f;
            onJump.Invoke();
            do
            {
                _verticalVelocity = jumpForce * _jumpCurve.Evaluate(time / inAirTime);

                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            while (time <= inAirTime || !_onGround);
            onLanding.Invoke();
            _verticalVelocity = 0;
        }

        private void IsPlayerOnGround()
        {
            _onGround = Physics2D.Raycast(transform.position, Vector2.down, _collider.radius + 0.05f, _groundLayers);
        }

    }
}