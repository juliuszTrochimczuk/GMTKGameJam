using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private Transform _grabCheck;
        [SerializeField] private float _gradRadius;
        [SerializeField] private LayerMask _whatIsGrabbable;
        [SerializeField] private float _grabDistance = 3f;

        private Rigidbody2D _heldObject;
        private MousePoint _mousePoint;

        private void Awake()
        {
            _mousePoint = GetComponentInChildren<MousePoint>();
        }


        private void FixedUpdate()
        {
            if (_heldObject == null)
                return;

            _holdPoint.transform.position = (_mousePoint.transform.position - transform.position).normalized * _grabDistance + transform.position;
            _heldObject.gravityScale = 0f;
            _heldObject.velocity = (_holdPoint.position - _heldObject.transform.position) * 20;
        }

        public void Grab(CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (_heldObject == null)
                {
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _gradRadius, _whatIsGrabbable);

                    if (hitColliders.Length <= 0)
                        return;

                    _heldObject = hitColliders[0].gameObject.GetComponent<Rigidbody2D>();
                }
                else
                {
                    _heldObject.gravityScale = 1f;
                    _heldObject = null;
                }
            }
        }
    }
}