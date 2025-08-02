using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerGrabber : MonoBehaviour
    {
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private float _gradRadius;
        [SerializeField] private LayerMask _whatIsGrabbable;
        [SerializeField] private float _grabDistance = 3f;
        [SerializeField] private float _grabMagnitude = 2f;
        [SerializeField] private float _normalGravityScale = 5f;
        [SerializeField] private Transform _mousePoint;
        private Rigidbody2D _heldObject;
        

        


        private void FixedUpdate()
        {
            

            RaycastHit2D hit = Physics2D.Raycast(transform.position, _mousePoint.transform.position, _grabDistance, 8);
            if (hit.collider != null)
                _holdPoint.position = hit.point;
            else
                _holdPoint.transform.position = (_mousePoint.transform.position - transform.position).normalized * _grabDistance + transform.position;
            if (_heldObject != null)
            {
                _heldObject.gravityScale = 0f;
                _heldObject.velocity = (_holdPoint.position - _heldObject.transform.position) * _grabMagnitude;
            }
           
        }

        public void Grab(CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (_heldObject == null)
                {
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_holdPoint.position, _gradRadius, _whatIsGrabbable);

                    if (hitColliders.Length <= 0)
                        return;

                    _heldObject = hitColliders[0].gameObject.GetComponent<Rigidbody2D>();
                    _heldObject.gameObject.layer = LayerMask.NameToLayer("GrabbedObject");
                }
                else
                {
                    _heldObject.gameObject.layer = LayerMask.NameToLayer("MovableObject");
                    _heldObject.gravityScale = _normalGravityScale;
                    _heldObject = null;
                }
            }
        }
    }
}