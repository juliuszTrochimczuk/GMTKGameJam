using UnityEngine;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        public bool facingRight = false;
        
        private Animator _animator;
        private Rigidbody2D _rb;
        private PlayerCharacterController _playerCharacterController;
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _playerCharacterController = GetComponent<PlayerCharacterController>();
        }
        
        public void FacingCheck(float xInput)
        {
            if (facingRight == false && xInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && xInput < 0)
            {
                Flip();
            }
        }
        
        public void Flip()
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        
        public void IdleAnimation()
        {
        }

        public void WalkAnimation()
        {
        }

        public void JumpAnimation()
        {
        }
    }
}
