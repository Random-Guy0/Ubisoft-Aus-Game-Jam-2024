using System.Collections;
using System.Collections.Generic;
using Jam.Entities;
using UnityEngine;

using Jam.Managers;

namespace Jam.StateMachine.Walker
{
    /// <summary>
    /// State machine controller for walker.
    /// </summary>
    /// 
    public class StateMachineController_Walker : StateMachineController
    {
        [SerializeField] private RuntimeAnimatorController[] potentialAnimators;
        protected override State entryState { get { return new State_Walker_Move(); } }

        private float[] speedRange = { 2.5f, 4.0f };
        private float speed;
  
        public float Speed { get { return speed; } }

        protected override void Awake()
        {
            speed = Random.Range(speedRange[0], speedRange[1]);
            base.Awake();
            
            int randomAnimator = Random.Range(0, potentialAnimators.Length);
            entity.Animator.runtimeAnimatorController = potentialAnimators[randomAnimator];
        }

        public override void OnRemoved()
        {
            PlayManager.Instance.WalkerCount--;
        }

        protected override void Update()
        {
            base.Update();
            Vector2 velocity = entity.RigidBody.velocity;
            entity.Animator.SetFloat("MoveX", velocity.x);
            entity.Animator.SetFloat("MoveY", velocity.y);
        }
    }
}

