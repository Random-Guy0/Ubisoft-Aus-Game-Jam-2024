using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

using Jam.Entities;

namespace Jam.StateMachine.Vehicle
{
    [RequireComponent(typeof(NavMeshObstacle))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StateMachineController_Vehicle : StateMachineController
    {
        NavMeshObstacle navObstacle;
        BoxCollider2D frontTriggerBox;

        protected override State entryState { get { return new State_Vehicle_Move(); } }

        public float MaxVelocity = 6.0f;
        public Vector2 Direction = new Vector2(1.0f, 0.0f);
        


        protected override void Awake()
        {
            navObstacle = GetComponent<NavMeshObstacle>();
            navObstacle.carveOnlyStationary = false;
            navObstacle.carving = true;
            

            base.Awake();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Entity>())
            {
                NotifyState(new Notification_Vehicle_Entity_InFront());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Entity>())
            {
                NotifyState(new Notification_Vehicle_Entity_Away());
            }
        }


    }

}
