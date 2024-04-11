using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Jam.Entities.Enemy;
using Jam.Managers;

namespace Jam.StateMachine.Walking_Tosser
{
    public class StateMachineController_Walking_Tosser : StateMachineController
    {
        protected override State entryState { get { return new State_Walking_Tosser_Walk(); } }

        private float[] speedRange = { 2.0f, 4.0f };
        private float speed = 0.0f;

        private bool tossed = false;
        private float tossDelay = 3.0f;

        public float Multiplier = 1.0f;

        public float Speed { get { return speed; } set { speed = value; } }
        public bool Tossed { get { return tossed; } set { tossed = value; } }

        public float TossDelay { get { return tossDelay; } }


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



        protected override void Awake()
        {
            speed = Random.Range(speedRange[0], speedRange[1]);

            base.Awake();
        }

        public override void OnRemoved()
        {
            PlayManager.Instance.WalkingTosserCount--;
        }
    }

}