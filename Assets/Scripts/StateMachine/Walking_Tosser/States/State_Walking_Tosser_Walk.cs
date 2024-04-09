using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine.Generic;
using Jam.Entities;

namespace Jam.StateMachine.Walking_Tosser
{
    public class State_Walking_Tosser_Walk : State<StateMachineController_Walking_Tosser, Entity>
    {

        private float tossDelay;
        private float enterTime;

        public override void OnEnter()
        {
            entity.RigidBody.velocity = controller.Direction * controller.Speed;

            enterTime = Time.time;
            tossDelay = Random.Range(1.5f, 3.0f);

            Debug.Log("Imma walk", entity.gameObject);
        }

        public override void OnExit()
        {
            
        }

        public override void OnNotify(Notification notification)
        {
            if(notification is Notification_Attacked)
            {
                controller.ChangeState(new State_Walking_Tosser_Attacked());
            }
        }

        public override void OnUpdate()
        {
            if(Time.time - enterTime >= tossDelay && !controller.Tossed)
            {
                controller.ChangeState(new State_Walking_Tosser_Toss());
            }
           
        }
    }
}
