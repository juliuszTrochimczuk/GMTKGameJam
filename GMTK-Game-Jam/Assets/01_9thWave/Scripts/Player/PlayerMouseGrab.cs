using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01_9thWave.Scripts.Player
{
    public class PlayerGrabMouse : MonoBehaviour
    {
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private LayerMask _whatIsGrabbable;
        [SerializeField] private float _grabDistance = 3f;
        [SerializeField] private float _grabMagnitude = 2f;
        [SerializeField] private float _normalGravityScale = 5f;
        [SerializeField] private Transform _mousePoint;
        [SerializeField] private Texture2D _grabCursorTexture;
        [SerializeField] private Texture2D _normalCursorTexture;
        [SerializeField] private Texture2D _openHandCursorTexture;
        
        private Rigidbody2D _heldObject;
        private PlayerHandsAnimator _playerHandsAnimator;


        private void Start()
        {
            _playerHandsAnimator = GetComponent<PlayerHandsAnimator>();
        }

        private void Update()
        {
            _holdPoint.position = CalculateGrabPointPosition();
            if (_heldObject != null)
            {
                _heldObject.gravityScale = 0f;
                _heldObject.velocity = (_holdPoint.position - _heldObject.transform.position) * _grabMagnitude;
            }
            Grab();
            
        }

        private Vector2 CalculateGrabPointPosition()
        {
            Vector2 holdPoint;
            if (Vector2.Distance(_mousePoint.position, transform.position) > _grabDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, _mousePoint.transform.position, _grabDistance, 0, 1);
                if (hit.collider != null)
                    holdPoint = hit.point;
                else
                    holdPoint = (_mousePoint.transform.position - transform.position).normalized * _grabDistance + transform.position;

            }
            else
            {
                holdPoint = _mousePoint.position;
            }
            return holdPoint;
        }

        public void Grab()
        {
            
            Collider2D hit = Physics2D.OverlapPoint(_holdPoint.position, _whatIsGrabbable);
            if (hit != null)
            { 
                Cursor.SetCursor(_openHandCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (_heldObject == null)
            {
                Cursor.SetCursor(_normalCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (hit != null)
                {
                    _playerHandsAnimator.SwichClawState();
                    _heldObject = hit.GetComponent<Rigidbody2D>();
                    _heldObject.gameObject.layer = LayerMask.NameToLayer("GrabbedObject");
                    Cursor.SetCursor(_grabCursorTexture, Vector2.zero, CursorMode.Auto);
                }
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                if (_heldObject != null)
                {
                    _playerHandsAnimator.SwichClawState();
                    _heldObject.gameObject.layer = LayerMask.NameToLayer("MovableObject");
                    _heldObject.gravityScale = _normalGravityScale;
                }
                _heldObject = null;
            }
        }
    }
}
