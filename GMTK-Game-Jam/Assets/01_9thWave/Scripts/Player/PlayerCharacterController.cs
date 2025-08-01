using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace _01_9thWave.Scripts.Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] int playerSpeed = 10;
        [SerializeField] private int playerJumpForce = 10;

        [SerializeField] private Transform _holdPoint;
        [SerializeField] private Transform _grabCheck;
        [SerializeField] private float _gradRadius;
        [SerializeField] private LayerMask _whatIsGrabbable;
        [SerializeField] private float _grabDistance = 3f;
        [SerializeField] private MousePoint _mousePoint;


        private bool _isGrounded;
        private bool _isGrabbing = false;
        
        private Vector2 _inputVector;
        private GameObject _heldObject;
        
        private Rigidbody2D _rb;
        private CircleCollider2D collider;
        private PlayerAnimator _animator;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<CircleCollider2D>();
            _animator = GetComponent<PlayerAnimator>();
        }

        void FixedUpdate()
        {
            CheckIfGrounded();
            _animator.FacingCheck(_inputVector.x);
            _rb.velocity = new Vector2(_inputVector.x * playerSpeed, _rb.velocity.y);
            if (_inputVector.x != 0) Walk(); else Idle();
            
            if (_heldObject != null)
            {
               _holdPoint.transform.position = (_mousePoint.transform.position-transform.position).normalized*_grabDistance + transform.position;
                _heldObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                _heldObject.GetComponent<Rigidbody2D>().velocity =
                    (_holdPoint.position - _heldObject.transform.position) * 20;
            }
        }

        public void MoveInputValues(CallbackContext ctx) => _inputVector = ctx.ReadValue<Vector2>();
        
        private void Walk()
        {
            _animator.WalkAnimation();
        }
        
        private void Idle()
        {
            _animator.IdleAnimation();
        }

        public void Jump(CallbackContext ctx)
        {
            if (_isGrounded)
            {
                _animator.JumpAnimation();
                Jump();
            }
        }

        private void Jump() => 
            _rb.velocity = Vector2.up * playerJumpForce;

        private void CheckIfGrounded() => _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, collider.radius + 0.05f);

        public void Grab(CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (_isGrabbing == false)
                {
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _gradRadius, _whatIsGrabbable);

                    if (hitColliders.Length > 0)
                    {
                        GameObject objectToGrab = hitColliders[0].gameObject;
                        _heldObject = objectToGrab;
                        _heldObject.layer = LayerMask.NameToLayer("GrabbedObject");
                        _isGrabbing = true;
                    }
                }
                else if (_isGrabbing == true)
                {
                    _heldObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    _heldObject.layer = LayerMask.NameToLayer("MovableObject");
                    _heldObject = null;
                    _isGrabbing = false;
                }
            }
        }
    }
}