using System.Collections;
using System.Collections.Generic;
using Jam.Entities;
using UnityEngine;
using UnityEngine.AI;

using Jam.Entities.Enemy;
using Jam.Entities.Trash;
using Jam.Managers;

namespace Jam.StateMachine.Walking_Tosser
{
    public class StateMachineController_Walking_Tosser : StateMachineController
    {
        [SerializeField] private RuntimeAnimatorController[] potentialAnimators;
        [SerializeField] private Trash trash;
        
        protected override State entryState { get { return new State_Walking_Tosser_Walk(); } }

        private float[] speedRange = { 2.0f, 4.0f };
        private float speed = 0.0f;

        private bool tossed = false;
        private float tossDelay = 3.0f;

        public float Multiplier = 1.0f;

        public float Speed { get { return speed; } set { speed = value; } }
        public bool Tossed { get { return tossed; } set { tossed = value; } }

        public float TossDelay { get { return tossDelay; } }
        
        public void TossTrash()
        {
            Instantiate(trash, transform.position, Quaternion.identity);
        }



        protected override void Awake()
        {
            speed = Random.Range(speedRange[0], speedRange[1]);

            base.Awake();
            
            int randomAnimator = Random.Range(0, potentialAnimators.Length);
            entity.Animator.runtimeAnimatorController = potentialAnimators[randomAnimator];
        }

        public override void OnRemoved()
        {
            PlayManager.Instance.WalkingTosserCount--;
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