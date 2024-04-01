using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.StateMachine.Walker
{
    /// <summary>
    /// State machine controller for walker.
    /// </summary>
    public class StateMachineController_Walker : StateMachineController
    {
        protected override State entryState { get { return new State_Walker_Move(); } }

        private float minSpeed = 3.0f;
        private float maxSpeed = 7.0f;
        private float speed;
  
        public Vector2 Direction { get; set; } = Vector2.right;
        public float Speed { get { return speed; } }

        protected override void Awake()
        {
            speed = Random.Range(minSpeed, maxSpeed);
            base.Awake();
        }

    }
}

