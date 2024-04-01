using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Walking_Tosser
{
    public class State_Walking_Tosser_Toss : State<StateMachineController_Walking_Tosser, Entity>
    {
        private float enterTime;

        public override void OnEnter()
        {
            entity.RigidBody.velocity = Vector2.zero;

            enterTime = Time.time;
            Debug.Log("Imma toss some trash", entity.gameObject);
        }

        public override void OnExit()
        {
            
        }

        public override void OnNotify(Notification notification)
        {
           
        }

        public override void OnUpdate()
        {
            float t = Time.time - enterTime;
            if(Time.time - enterTime > controller.TossDelay)
            {
                controller.Tossed = true;
                Debug.Log("I tossed", entity.gameObject);
                controller.TossTrash();

                controller.ChangeState(new State_Walking_Tosser_Walk());
            }


        }
    }

}
