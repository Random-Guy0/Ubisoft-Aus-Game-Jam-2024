using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Managers;

namespace Jam.StateMachine.Tosser
{
    /// <summary>
    /// Tosser will attempt to throw out up to n trashes before leaving the scene.
    /// </summary>

    public class StateMachineController_Tosser : StateMachineController
    {
        protected override State entryState { get { return new State_Tosser_Idle(); } }

        private float[] speedRange = { 3.0f, 5.0f }; 
        private float speed = 0.0f;

        protected override void Awake()
        {
            speed = Random.Range(speedRange[0], speedRange[1]);

            base.Awake();
        }


        [SerializeField]
        Sprite sprite;
        // Temp Func for demonstration purpose
        public void TossTrash()
        {
            var obj = new GameObject();
            obj.AddComponent<SpriteRenderer>().sprite = sprite;
            obj.transform.localScale = Vector3.one * 0.1f;

            obj.transform.position = entity.transform.position;
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
    }

}
