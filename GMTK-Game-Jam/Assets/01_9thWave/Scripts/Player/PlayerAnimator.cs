using UnityEngine;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private PlayerCharacterController _playerCharacterController;
        private Animator _animator;
        
        public bool facingRight = false;
        
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
    }
}
