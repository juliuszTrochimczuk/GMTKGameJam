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
        //for slope
        [SerializeField] private float _groundRaycastSpacing = 0.3f;

        private Vector2 _groundNormal = Vector2.up;
        private float _slopeRayLength = 0.2f;
        
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
        [SerializeField] private float _targetGravScale = 9.89f;
        [SerializeField] private float _minFallSpeed = -5f; // ensures you fall fast when stepping off
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

            _rb.gravityScale = _onGround ? 1 : _targetGravScale;
            
            // If just walked off a ledge, ensure we start falling fast
            if (!_onGround && _verticalVelocity == 0)
            {
                _verticalVelocity = _minFallSpeed;
            }

            _horizontalVelocity = Mathf.SmoothDamp(_horizontalVelocity, InputDirection, ref _currentHorizontalVelocity, _MoveSmoother);

            if (_canMove)
            {
                Vector2 velocity;
                if (_onGround)
                {
                    Vector2 slopeDirection = new Vector2(_groundNormal.y, -_groundNormal.x).normalized;
                    float speed = _horizontalVelocity * _MaxSpeed;
                    velocity = slopeDirection * speed;
                    velocity.y += _verticalVelocity;
                }
                else
                {
                    velocity = new Vector2(_horizontalVelocity * _MaxSpeed, _verticalVelocity);
                }

                _rb.velocity = velocity;
            }

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
            //_rb.velocity = Vector2.zero;
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
            Vector2 origin = transform.position;

            Vector2 leftOrigin = origin + Vector2.left * _groundRaycastSpacing;
            Vector2 rightOrigin = origin + Vector2.right * _groundRaycastSpacing;

            RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, _collider.radius + _slopeRayLength, _groundLayers);
            RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, _collider.radius + _slopeRayLength, _groundLayers);

            if (leftHit && rightHit)
            {
                _onGround = true;
                _groundNormal = ((leftHit.normal + rightHit.normal) * 0.5f).normalized;
            }
            if (leftHit)
            {
                _onGround = true;
                _groundNormal = leftHit.normal;
            }
            else if (rightHit)
            {
                _onGround = true;
                _groundNormal = rightHit.normal;
            }
            else
            {
                _onGround = false;
                _groundNormal = Vector2.up;
            }
        }

        
        private void OnDrawGizmos()
        {
            if (_collider == null)
                _collider = GetComponent<CircleCollider2D>();

            Gizmos.color = Color.green;

            Vector2 origin = transform.position;
            Vector2 leftOrigin = origin + Vector2.left * _groundRaycastSpacing;
            Vector2 rightOrigin = origin + Vector2.right * _groundRaycastSpacing;
            float rayLength = _collider.radius + _slopeRayLength;

            // Left ray
            Gizmos.DrawLine(leftOrigin, leftOrigin + Vector2.down * rayLength);
            // Right ray
            Gizmos.DrawLine(rightOrigin, rightOrigin + Vector2.down * rayLength);

#if UNITY_EDITOR
            // Optionally draw normals live if the game is playing
            if (Application.isPlaying)
            {
                // Left
                RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, rayLength, _groundLayers);
                if (leftHit)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(leftHit.point, leftHit.point + leftHit.normal * 0.5f);
                }

                // Right
                RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, rayLength, _groundLayers);
                if (rightHit)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(rightHit.point, rightHit.point + rightHit.normal * 0.5f);
                }
            }
#endif
        }

    }
}