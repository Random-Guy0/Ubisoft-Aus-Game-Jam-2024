using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Jam.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public bool CanMove { get; set; } = true;

        [SerializeField] private float speed = 5f;
        [SerializeField] [Range(0f, 1f)] private float acceleration = 0.4f;

        private Rigidbody2D _rb;
        private Vector2 _moveInput = Vector2.zero;
        private Vector2 _velocity = Vector2.zero;

        public void Init(Rigidbody2D rb)
        {
            _rb = rb;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            bool moving = _moveInput != Vector2.zero;

            //accelerate and move
            if (CanMove && moving)
            {
                Vector2 moveAmount = _moveInput * speed;
                _velocity = Vector2.Lerp(_velocity, moveAmount, acceleration);
                Vector2 absoluteVelocity = _velocity.Abs();
                if (Vector2.Max(moveAmount.Abs() - (Vector2.one * 0.01f), absoluteVelocity) == absoluteVelocity)
                {
                    _velocity = moveAmount;
                }
            }

            //decelerate and stop
            if (!moving || !CanMove)
            {
                _velocity = Vector2.Lerp(_velocity, Vector2.zero, acceleration);
                Vector2 absoluteVelocity = _velocity.Abs();
                if (Vector2.Min(Vector2.one * 0.01f, absoluteVelocity) == absoluteVelocity)
                {
                    _velocity = Vector2.zero;
                }
            }

            _rb.velocity = _velocity;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 newMoveInput = context.ReadValue<Vector2>();
            
            if (Mathf.Abs(newMoveInput.x) > Mathf.Abs(_moveInput.x))
            {
                newMoveInput.y = 0;
            }
            else if (Mathf.Abs(newMoveInput.y) > Mathf.Abs(_moveInput.y))
            {
                newMoveInput.x = 0;
            }

            _moveInput = newMoveInput;
        }
    }
}
