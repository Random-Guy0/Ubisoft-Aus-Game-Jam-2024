using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


namespace Jam.Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public bool CanMove { get; set; } = true;

        [SerializeField] private float speed = 5f;
        [SerializeField] [Range(0f, 1f)] private float acceleration = 0.4f;

        private Player entity;
        private Vector2 _moveInput = Vector2.zero;
        private Vector2 _velocity = Vector2.zero;


        private void Awake()
        {
            entity = GetComponent<Player>();
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
            if (!moving)
            {
                _velocity = Vector2.Lerp(_velocity, Vector2.zero, acceleration);
                Vector2 absoluteVelocity = _velocity.Abs();
                if (Vector2.Min(Vector2.one * 0.01f, absoluteVelocity) == absoluteVelocity)
                {
                    _velocity = Vector2.zero;
                }
            }

            entity.RigidBody.velocity = _velocity;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
    }
}



