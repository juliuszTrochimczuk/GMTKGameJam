using System;
using System.Collections;
using _01_9thWave.Scripts.Audio;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;
using Random = UnityEngine.Random;

namespace _01_9thWave.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        // Slope detection
        [SerializeField] private float _groundRaycastSpacing = 0.3f;
        [SerializeField] private LayerMask _groundLayers;
        private Vector2 _groundNormal = Vector2.up;
        private float _slopeRayLength = 0.2f;

        // Events
        [SerializeField] private UnityEvent onJump;
        [SerializeField] private UnityEvent onLanding;
        [SerializeField] private UnityEvent<float> onChangingDirection;

        [Header("Walking")]
        [SerializeField] private float _groundMaxSpeed;
        [SerializeField] private float _groundMoveSmoother;
        [SerializeField] private float _timeBetweenSteps;

        [Header("Jumping")]
        [SerializeField] private float _inAirMaxSpeed;
        [SerializeField] private float _inAirMoveSmoother;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _maxJumpDuration;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _minFallSpeed = -5f;


        private Rigidbody2D _rb;
        private CircleCollider2D _collider;

        // Movement state
        public float InputDirection { get; private set; }
        private bool _onGround;
        private bool _canMove = true;
        private float _walkTimer;

        // Jumping state
        private bool _isJumping;
        private float _jumpTimer;

        private float _currentHorizontalVelocity;

        private float MaxSpeed => _onGround ? _groundMaxSpeed : _inAirMaxSpeed;
        private float MoveSmoother => _onGround ? _groundMoveSmoother : _inAirMoveSmoother;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
        }

        private void FixedUpdate()
        {
            
            DetectGround();
            UpdateJumpState();
            MoveCharacter();
            HandleFootsteps();
        }

        private void Update()
        {
            if (_onGround && !_isJumping && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jumping start");
                StartJump();
            }
        }

        public void ReadMoveInputVector(CallbackContext ctx)
        {
            InputDirection = ctx.ReadValue<float>();
            onChangingDirection.Invoke(InputDirection);
        }
        


        public void Jump(float force, float duration)
        {
            StartJump();
        }

        public void StunPlayer(float delay)
        {
            StartCoroutine(DisableMovement(delay));
        }

        private IEnumerator DisableMovement(float delay)
        {
            _canMove = false;
            yield return new WaitForSeconds(delay);
            _canMove = true;
        }

        private void StartJump()
        {
            _isJumping = true;
            _jumpTimer = 0f;
            onJump.Invoke();
        }

        private void UpdateJumpState()
        {
            if (_isJumping)
            {
                _jumpTimer += Time.fixedDeltaTime;
                if (_jumpTimer >= _maxJumpDuration)
                {
                    EndJump();
                }
            }
            else if (!_onGround && _rb.velocity.y <= 0f)
            {
                // Falling after apex
                _isJumping = false;
            }
        }

        private void EndJump()
        {
            _isJumping = false;
        }

        private void MoveCharacter()
        {
            if (!_canMove) return;

            // Horizontal smoothing based on input
            float targetH = InputDirection;
            float smoothH = Mathf.SmoothDamp(_rb.velocity.x / MaxSpeed, targetH, ref _currentHorizontalVelocity, MoveSmoother);
            float horizVel = smoothH * MaxSpeed;

            // Vertical velocity via jump curve or natural fall
            float vertVel;
            if (_isJumping)
            {
                float eval = _jumpCurve.Evaluate(_jumpTimer / _maxJumpDuration);
                vertVel = _jumpForce * eval;
            }
            else if (!_onGround && _rb.velocity.y <= 0.01f)
            {
                vertVel = Mathf.Lerp(_rb.velocity.y, _minFallSpeed, MoveSmoother);
            }
            else
            {
                vertVel = _rb.velocity.y;
            }

            // Slope adjustment if grounded
            Vector2 finalVel;
            if (_onGround)
            {
                Vector2 slopeDir = new Vector2(_groundNormal.y, -_groundNormal.x).normalized;
                finalVel = slopeDir * horizVel;
                finalVel.y = vertVel;
            }
            else
            {
                finalVel = new Vector2(horizVel, vertVel);
            }

            _rb.velocity = finalVel;
        }

        private void HandleFootsteps()
        {
            _walkTimer += Time.fixedDeltaTime;
            if (_onGround && Mathf.Abs(InputDirection) > 0.01f && _walkTimer >= _timeBetweenSteps)
            {
                _walkTimer = Random.Range(0f, 0.2f);
                AudioManager.Instance.PlayFootstepEffects();
            }
        }

        private void DetectGround()
        {
            Vector2 origin = transform.position;
            Vector2 left = origin + Vector2.left * _groundRaycastSpacing;
            Vector2 right = origin + Vector2.right * _groundRaycastSpacing;
            float len = _collider.radius + _slopeRayLength;

            RaycastHit2D hitL = Physics2D.Raycast(left, Vector2.down, len, _groundLayers);
            RaycastHit2D hitR = Physics2D.Raycast(right, Vector2.down, len, _groundLayers);

            _onGround = false;
            if (hitL && hitR)
            {
                _onGround = true;
                _groundNormal = (hitL.normal + hitR.normal).normalized;
            }
            else if (hitL)
            {
                _onGround = true;
                _groundNormal = hitL.normal;
            }
            else if (hitR)
            {
                _onGround = true;
                _groundNormal = hitR.normal;
            }
            else
            {
                _groundNormal = Vector2.up;
            }
        }

        private void OnDrawGizmos()
        {
            if (_collider == null) _collider = GetComponent<CircleCollider2D>();
            Gizmos.color = Color.green;
            Vector2 origin = transform.position;
            Vector2 left = origin + Vector2.left * _groundRaycastSpacing;
            Vector2 right = origin + Vector2.right * _groundRaycastSpacing;
            float len = _collider.radius + _slopeRayLength;

            Gizmos.DrawLine(left, left + Vector2.down * len);
            Gizmos.DrawLine(right, right + Vector2.down * len);

#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                RaycastHit2D hitL = Physics2D.Raycast(left, Vector2.down, len, _groundLayers);
                if (hitL) { Gizmos.color = Color.red; Gizmos.DrawLine(hitL.point, hitL.point + hitL.normal * 0.5f); }
                RaycastHit2D hitR = Physics2D.Raycast(right, Vector2.down, len, _groundLayers);
                if (hitR) { Gizmos.color = Color.red; Gizmos.DrawLine(hitR.point, hitR.point + hitR.normal * 0.5f); }
            }
#endif
        }
    }
}
