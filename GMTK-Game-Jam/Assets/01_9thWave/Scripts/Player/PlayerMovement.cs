using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Walking")]
        [SerializeField] private float _groundMaxSpeed;
        [SerializeField] private float _groundMoveSmoother;

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
        private float _inputDirection;

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

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
        }

        private void FixedUpdate()
        {
            IsPlayerOnGround();

            _horizontalVelocity = Mathf.SmoothDamp(_horizontalVelocity, _inputDirection, ref _currentHorizontalVelocity, _MoveSmoother);
            _rb.velocity = new Vector2(_horizontalVelocity * _MaxSpeed, _verticalVelocity);
        }

        public void ReadMoveInputVector(CallbackContext ctx) => _inputDirection = ctx.ReadValue<float>();

        public void Jump(CallbackContext ctx)
        {
            if (_onGround && ctx.performed)
                StartCoroutine(Jumping());
        }

        private IEnumerator Jumping()
        {
            float time = 0.0f;
            do
            {
                _verticalVelocity = _jumpForce * _jumpCurve.Evaluate(time / maxTimeInAir);

                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            while (time <= maxTimeInAir || !_onGround);

            _verticalVelocity = 0;
        }

        private void IsPlayerOnGround() =>
            _onGround = Physics2D.Raycast(transform.position, Vector2.down, _collider.radius + 0.05f, LayerMask.GetMask("Default", "Ground"));
    }
}