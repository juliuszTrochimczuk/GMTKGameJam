using UnityEngine;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private string jumpTrigger = "Jump";
        private string walkingBool = "isWalking";
        
        public bool facingRight = false;
        
        private Animator _animator;
        private Rigidbody2D _rb;
        private CharacterController _playerCharacterController;
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _playerCharacterController = GetComponent<CharacterController>();
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
            _animator.SetBool(walkingBool, false);
        }

        public void WalkAnimation()
        {
            _animator.SetBool(walkingBool, true);
        }

        public void JumpAnimation()
        {
            _animator.SetTrigger(jumpTrigger);
        }
    }
}
