using System.Collections;
using System.Collections.Generic;
using Jam.Entities;
using Jam.Entities.Trash;
using UnityEngine;

using Jam.Managers;

namespace Jam.StateMachine.Tosser
{
    /// <summary>
    /// Tosser will attempt to throw out up to n trashes before leaving the scene.
    /// </summary>

    public class StateMachineController_Tosser : StateMachineController
    {
        [SerializeField] private Trash trash;
        [SerializeField] private RuntimeAnimatorController[] potentialAnimators;
        
        protected override State entryState { get { return new State_Tosser_Move(); } }

        private float[] speedRange = { 3.0f, 5.0f }; 
        private float speed = 0.0f;

        private Vector2 _previousPosition;
  

        protected override void Awake()
        {
            speed = Random.Range(speedRange[0], speedRange[1]);

            base.Awake();

            int randomAnimator = Random.Range(0, potentialAnimators.Length);
            entity.Animator.runtimeAnimatorController = potentialAnimators[randomAnimator];
            _previousPosition = entity.transform.position;
        }
        
        public void TossTrash()
        {
            Instantiate(trash, transform.position, Quaternion.identity);
        }



        /// <summary>
        /// How many times can Tosser transition between idle and move before forced throw out a trash
        /// </summary>
        public int MaxMoveTransitionCount { get; } = 3;
        public int CurrentMoveTransitionCount { get; set; } = 0;
        public float Speed { get { return speed; } }

        /// <summary>
        /// No. of trashes the tosser currently holds.
        /// </summary>
        public int TossCount { get; set; } = 3;


        /// <summary>
        /// Check if this tosser should force-transition to toss state
        /// </summary>
        /// <returns></returns>
        public bool ShouldForceToss()
        {
            return CurrentMoveTransitionCount >= MaxMoveTransitionCount;
        }

        public override void OnRemoved()
        {
            PlayManager.Instance.TosserCount--;
        }

        protected override void Update()
        {
            base.Update();
            Vector2 position = entity.transform.position;
            Vector2 velocity = (position - _previousPosition) / Time.deltaTime;
            if (Mathf.Abs(velocity.x) >= Mathf.Abs(velocity.y))
            {
                velocity.y = 0f;
            }
            else
            {
                velocity.x = 0f;
            }
            entity.Animator.SetFloat("MoveX", velocity.x);
            entity.Animator.SetFloat("MoveY", velocity.y);
            _previousPosition = position;
        }
    }

}
