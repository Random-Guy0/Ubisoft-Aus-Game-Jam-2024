using System;
using System.Collections;
using System.Collections.Generic;using Jam.Entities;
using Jam.Entities.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jam.Player
{

    [RequireComponent(typeof(PlayerController), typeof(PlayerAttackHandler), typeof(PlayerInput))]
    public class Player : Entity
    {
        public PlayerController PlayerController { get; private set; }
        public PlayerAttackHandler PlayerAttackHandler { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            PlayerController = GetComponent<PlayerController>();
            PlayerAttackHandler = GetComponent<PlayerAttackHandler>();
        }

        private void Start()
        {
            PlayerController.Init(RigidBody);
            PlayerAttackHandler.Init(PlayerController);
        }
    }
}
