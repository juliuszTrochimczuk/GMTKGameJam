using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace _01_9thWave.Scripts.Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] int playerSpeed = 10;
        [SerializeField] private int playerJumpForce = 10;

        [SerializeField] private Transform _holdPoint;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _checkRadius;
        [SerializeField] private Transform _grabCheck;
        [SerializeField] private float _gradRadius;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private LayerMask _whatIsGrabbable;
        [SerializeField] private float _grabDistance = 3f;

        private Rigidbody2D _rb;
        private MousePoint _mousePoint;

        private bool _isGrounded;
        private bool _isGrabbing = false;
        
        private Vector2 _inputVector;
        private GameObject _heldObject;

        private void Start()
        {
            _mousePoint = GetComponentInChildren<MousePoint>();
            _rb = GetComponent<Rigidbody2D>();
           }

        void FixedUpdate()
        {
            CheckIfGrounded();
            _rb.velocity = new Vector2(_inputVector.x * playerSpeed, _rb.velocity.y);
            
            if (_heldObject != null)
            {
               _holdPoint.transform.position = (_mousePoint.transform.position-transform.position).normalized*_grabDistance + transform.position;
                _heldObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                _heldObject.GetComponent<Rigidbody2D>().velocity =
                    (_holdPoint.position - _heldObject.transform.position) * 20;
            }
        }

        public void MoveInputValues(CallbackContext ctx) => _inputVector = ctx.ReadValue<Vector2>();

        public void Jump(CallbackContext ctx)
        {
            if (_isGrounded)
                Jump();
        }

        private void Jump()
        {
            _rb.velocity = Vector2.up * playerJumpForce;
        }

        private void CheckIfGrounded()
        {
            Vector2 groundCheck = _groundCheck.position;
            _isGrounded = (Physics2D.OverlapCircle(groundCheck, _checkRadius, _whatIsGround));
        }

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
                        _isGrabbing = true;
                    }
                }
                else if (_isGrabbing == true)
                {
                    _heldObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    _heldObject = null;
                    _isGrabbing = false;
                }
            }
        }
    }
}