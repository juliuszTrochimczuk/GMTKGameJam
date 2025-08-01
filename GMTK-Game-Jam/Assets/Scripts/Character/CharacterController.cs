using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _checkRadious;
        [SerializeField] private LayerMask _whatIsGround;
        
        [SerializeField] int playerSpeed = 10;
        [SerializeField] private int playerJumpForce = 10;
        
        private Rigidbody2D _rb;
        
        private float _xInput;
        private float _yInput;
        private bool _jumpInput;
        
        private bool _isGrounded;


        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _xInput = Input.GetAxis("Horizontal");
            _yInput = Input.GetAxis("Vertical");
            _jumpInput = Input.GetButton("Jump");
            
            if(_jumpInput && _isGrounded) {
                Jump();
            }
            
        }
        
        void FixedUpdate()
        {
            CheckIfGrounded();
            _rb.velocity = new Vector2(_xInput * playerSpeed, _rb.velocity.y);
        }
        
        private void Jump()
        {
            _rb.velocity = Vector2.up * playerJumpForce;
        }
        
        private void CheckIfGrounded()
        {
            Vector2 groundCheck = _groundCheck.position;
            _isGrounded = (Physics2D.OverlapCircle(groundCheck, _checkRadious, _whatIsGround));
        }
    }
}
