using _01_9thWave.Scripts.Player;
using UnityEngine;

namespace Objects
{
    public class JellyFish : MonoBehaviour
    {
        [SerializeField] private float _checkJumpRadius;
        [SerializeField] private Transform _checkJumpTransform;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _inAirTime;

        [Header("For the Gizmos")]
        [SerializeField] private Color _gizmosColor;
        [SerializeField] private bool _showGizmos;

        private Animator _animator;
        private PlayerMovement playerMovement;
        private bool _playerJumped;
        private float playerRadius;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            GameObject player = GameObject.FindWithTag("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
            playerRadius = player.GetComponent<CircleCollider2D>().radius;
        }

        private void Update()
        {
            if (Vector3.Distance(_checkJumpTransform.position - (Vector3.down * playerRadius), playerMovement.transform.position) > _checkJumpRadius)
            {
                _playerJumped = false;
                return;
            }

            if (!_playerJumped)
            {
                _animator.SetTrigger("Jump");
                playerMovement.Jump(_jumpForce, _inAirTime);
            }

            _playerJumped = true;
        }

        private void OnDrawGizmos()
        {
            if (!_showGizmos)
            {
                return;
            }

            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(_checkJumpTransform.position, _checkJumpRadius);
        }
    }
}