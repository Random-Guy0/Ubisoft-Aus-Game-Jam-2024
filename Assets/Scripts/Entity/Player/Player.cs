using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Jam.Entities.Player
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

            gameObject.layer = LAYER_PLAYER;
        }
    }
}
