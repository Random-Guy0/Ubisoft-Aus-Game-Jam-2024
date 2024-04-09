using System;
using System.Collections;
using System.Collections.Generic;
using Jam.AttackSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jam.Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttackHandler : AttackHandler
    {
        private Player _entity;

        protected override void Awake()
        {
            base.Awake();
            _entity = GetComponent<Player>();
        }

        private void Start()
        {
            AttackDirection = Vector2.right;
            AttackOrigin = transform.position;
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DoAttack();
            }
        }

        public void OnDirectionInput(InputAction.CallbackContext context)
        {
            Vector2 directionInput = context.ReadValue<Vector2>();

            if (directionInput != Vector2.zero)
            {
                if (Mathf.Abs(directionInput.x) > Mathf.Abs(AttackDirection.x))
                {
                    directionInput.y = 0;
                }
                else if (Mathf.Abs(directionInput.y) > Mathf.Abs(AttackDirection.y))
                {
                    directionInput.x = 0;
                }
                
                AttackDirection = directionInput;
            }
        }

        protected override void BeforeAttack()
        {
            _entity.PlayerController.CanMove = false;
        }

        protected override void AfterAttack()
        {
            _entity.PlayerController.CanMove = true;
        }

        private void Update()
        {
            AttackOrigin = transform.position;
        }
    }
}
