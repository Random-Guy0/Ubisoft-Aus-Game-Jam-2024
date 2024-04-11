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

        private Player _entity;
        private Vector2 _moveInput = Vector2.zero;
        public Vector2 Velocity { get; set; } = Vector2.zero;
        public bool PlayerHasMovementControl { get; set; } = true;


        private void Awake()
        {
            _entity = GetComponent<Player>(); 
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            Animate(_moveInput);
        }

        private void Move()
        {
            bool moving = _moveInput != Vector2.zero;

            if (PlayerHasMovementControl)
            {
                //accelerate and move
                if (CanMove && moving)
                {
                    Vector2 moveAmount = _moveInput * speed;
                    Velocity = Vector2.Lerp(Velocity, moveAmount, acceleration);
                    Vector2 absoluteVelocity = Velocity.Abs();
                    if (Vector2.Max(moveAmount.Abs() - (Vector2.one * 0.01f), absoluteVelocity) == absoluteVelocity)
                    {
                        Velocity = moveAmount;
                    }
                }

                //decelerate and stop
                if (!moving || !CanMove)
                {
                    Velocity = Vector2.Lerp(Velocity, Vector2.zero, acceleration);
                    Vector2 absoluteVelocity = Velocity.Abs();
                    if (Vector2.Min(Vector2.one * 0.01f, absoluteVelocity) == absoluteVelocity)
                    {
                        Velocity = Vector2.zero;
                    }
                }
            }

            _entity.RigidBody.velocity = Velocity;
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

        private void Animate(Vector2 movement)
        {
            if (!CanMove)
            {
                movement = Vector2.zero;
            }
            
            _entity.Animator.SetFloat("MoveX", movement.x);
            _entity.Animator.SetFloat("MoveY", movement.y);
        }
    }
}



