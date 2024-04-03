using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Walking_Tosser
{
    public class State_Walking_Tosser_Toss : State<StateMachineController_Walking_Tosser, Entity>
    {

        public override void OnEnter()
        {
            entity.RigidBody.velocity = Vector2.zero;

            Debug.Log("Imma toss some trash", entity.gameObject);
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

            if(GetTime() > controller.TossDelay)
            {
                controller.Tossed = true;
                Debug.Log("I tossed", entity.gameObject);
                controller.TossTrash();

                controller.ChangeState(new State_Walking_Tosser_Walk());
            }


        }
    }

}
