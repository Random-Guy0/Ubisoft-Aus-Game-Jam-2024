using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

using Jam.Managers;

namespace Jam.Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        AudioClip footstep1;
        [SerializeField]
        AudioClip footstep2;
        [SerializeField]
        AudioClip struck;

        [SerializeField]
        GameObject stunEffect;


        public bool CanMove { get; set; } = true;

        [SerializeField] private float speed = 5f;
        [SerializeField] [Range(0f, 1f)] private float acceleration = 0.4f;

        private Player _entity;
        private Vector2 _moveInput = Vector2.zero;
        public Vector2 Velocity { get; set; } = Vector2.zero;
        public bool PlayerHasMovementControl { get; set; } = true;

        private float stoppingAcc = 3.0f;
        float knockbackStartTime = 0.0f;


        // Up, Down, Left Right maps to 1, 2, 3, 4
        // For some reason On Move is called twice
        private List<int> hiddenCommand = new List<int>()
        {
            1, 1,
            2, 2,
            3, 4,
            3, 4,
        };

        int consecCommand = 0;
        bool ignoreCommand = false;

        bool lockInput = false;

        private void Awake()
        {
            _entity = GetComponent<Player>();
        }

        private void FixedUpdate()
        {
            if (lockInput)
                return;

            Move();
        }

        private void Update()
        {
            if (lockInput)
                return;

            Animate(_moveInput);
        }

        private void Move()
        {
            bool moving = _moveInput != Vector2.zero;

            if (PlayerHasMovementControl)
            {
                stunEffect.SetActive(false);

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

                _entity.RigidBody.velocity = Velocity;
                knockbackStartTime = Time.time;
            }
            else
            {
                stunEffect.SetActive(true);
                _entity.RigidBody.velocity -= _entity.RigidBody.velocity * stoppingAcc * Time.deltaTime;
           
                if(Time.time - knockbackStartTime > 1.0f)
                {
                    PlayerHasMovementControl = true;
                }
            }

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

            bool isZero = newMoveInput.x == 0 && newMoveInput.y == 0;

            if(!ignoreCommand && !isZero)
            {
                ignoreCommand = true;

                int move = 0;

                if(newMoveInput == Vector2.up)
                {
                    move = 1;
                }
                else if(newMoveInput == Vector2.down)
                {
                    move = 2;
                }
                else if(newMoveInput == Vector2.left)
                {
                    move = 3;
                }
                else if(newMoveInput == Vector2.right)
                {
                    move = 4;
                }

                if (hiddenCommand[consecCommand] == move)
                {
                    consecCommand++;

                    if(consecCommand >= hiddenCommand.Count)
                    {
                        consecCommand = 0;

                        // Look at Chain of responsibility go!
                        // (crunch edition)
                        if(PlayerHasMovementControl)
                        {
                            _entity.Animator.SetTrigger("DoDance");
                            _entity.RigidBody.velocity = Vector2.zero;
                            _entity.Animator.SetFloat("MoveX", 0);
                            _entity.Animator.SetFloat("MoveY", 0);
                            lockInput = true;
                        }


                    }

                }
                else
                {
                    consecCommand = 0;
                }
                



            }
            else if(!isZero)
            {
                ignoreCommand = false;
            }

            _moveInput = newMoveInput;
        }

        private void Animate(Vector2 movement)
        {
            if (!CanMove || !PlayerHasMovementControl)
            {
                movement = Vector2.zero;
            }

            
            _entity.Animator.SetFloat("MoveX", movement.x);
            _entity.Animator.SetFloat("MoveY", movement.y);
        }


        public void LockInput()
        {
            lockInput = true;
        }
        public void UnlockInput()
        {
            lockInput = false;
        }

        public void PlayFootstep1()
        {
            SoundManager.Instance.PlaySound(footstep1, this.gameObject, 0.8f);
        }

        public void PlayFootstep2()
        {
            SoundManager.Instance.PlaySound(footstep2, this.gameObject, 0.8f);
        }

        public void PlayStruck()
        {
            SoundManager.Instance.PlaySound(struck, gameObject.transform.position);
        }


    }
}



