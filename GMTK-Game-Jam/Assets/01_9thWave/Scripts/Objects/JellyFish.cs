using _01_9thWave.Scripts.Player;
using UnityEngine;

namespace Objects
{
    public class JellyFish : MonoBehaviour
    {
        [SerializeField] private Vector2 _checkJumpZone;
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
            if (!IsPlayerColliding())
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
            Gizmos.DrawCube(new(_checkJumpTransform.position.x, _checkJumpTransform.position.y), _checkJumpZone);
        }

        private bool IsPlayerColliding()
        {
            Vector2 playerCheckPosition = playerMovement.transform.position - (Vector3.down * playerRadius);
            return Mathf.Abs(playerCheckPosition.x - _checkJumpTransform.position.x) <= _checkJumpZone.x && 
                Mathf.Abs(playerCheckPosition.y - _checkJumpTransform.position.y) <= _checkJumpZone.y;
        }
    }
}