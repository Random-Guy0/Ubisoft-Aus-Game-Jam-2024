using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.Entities.Player
{

    [RequireComponent(typeof(PlayerController))]
    public class Player : Entity
    {

        public PlayerController PlayerController { get; set; }

        protected override void Awake()
        {
            base.Awake();
            PlayerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
          
            
        }
    }
}
