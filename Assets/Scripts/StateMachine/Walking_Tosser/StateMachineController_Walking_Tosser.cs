using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.StateMachine.Walking_Tosser
{
    public class StateMachineController_Walking_Tosser : StateMachineController
    {
        protected override State entryState { get { return new State_Walking_Tosser_Walk(); } }

        private float minSpeed = 3.0f;
        private float maxSpeed = 7.0f;
        private float speed = 0.0f;

        private bool tossed = false;
        private float tossDelay = 3.0f;

        public Vector2 Direction { get; set; } = Vector2.right;
        public float Speed { get { return speed; } }
        public bool Tossed { get { return tossed; } set { tossed = value; } }

        public float TossDelay { get { return tossDelay; } }


        [SerializeField]
        Sprite sprite;
        // Temp Func for demonstration purpose
        public void TossTrash()
        {
            var obj = new GameObject();
            obj.AddComponent<SpriteRenderer>().sprite = sprite;

            obj.transform.position = entity.transform.position;
        }



        protected override void Awake()
        {
            speed = Random.Range(minSpeed, maxSpeed);
            base.Awake();
        }

    }

}