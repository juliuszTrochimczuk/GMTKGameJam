using UnityEngine;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        
        void Awake() => _animator = GetComponent<Animator>();
        
        public void Flip(float direction)
        {
            if (direction > 0)
            {
                Vector3 scaler = transform.localScale;
                scaler.x = -1;
                transform.localScale = scaler;
            }
            else if (direction < 0)
            {
                Vector3 scaler = transform.localScale;
                scaler.x = 1;
                transform.localScale = scaler;
            }
        }

        public void WalkingAnimation(float walking) => _animator.SetBool("isWalking", Mathf.Abs(walking) > 0);

        public void ChangeJumpState(bool state) => _animator.SetBool("IsJumping", state);
    }
}
