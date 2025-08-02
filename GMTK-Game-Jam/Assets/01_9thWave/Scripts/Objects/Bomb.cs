using _01_9thWave.Scripts.Player;
using UnityEngine;

namespace Objects
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _velocityExplosionLevel;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionStrength;
        [SerializeField] private float _playerStunDuration;
        [SerializeField] private LayerMask _explosionMask;

        private Rigidbody2D _rb;
        private Collider2D _collider;
        private PlayerMovement playerMovement;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (_rb.velocity.magnitude < _velocityExplosionLevel)
            {
                return;
            }

            if (Vector2.Distance(transform.position, playerMovement.transform.position) <= _explosionRadius)
            {
                playerMovement.StunPlayer(_playerStunDuration);
            }

            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _explosionMask);
            foreach (Collider2D @object in detectedObjects)
            {
                if (@object == _collider || @object.CompareTag("Player"))
                    continue;

                Vector2 explosionDirection = @object.transform.position - transform.position;
                float explosionStrength = _explosionStrength * ((_explosionRadius - explosionDirection.magnitude)/ _explosionRadius);
                @object.GetComponent<Rigidbody2D>().AddForce((explosionDirection + Vector2.up) * explosionStrength);
            }
            //LATER ADD HERE SPAWNING EFFECT
            Destroy(gameObject);
        }
    }
}