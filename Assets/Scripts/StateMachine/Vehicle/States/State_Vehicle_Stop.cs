using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine.Generic;
using Jam.Entities;

namespace Jam.StateMachine.Vehicle
{
    public class State_Vehicle_Stop : State<StateMachineController_Vehicle, Entity>
    {
        private float dampVelocity;

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {
         
        }

        public override void OnNotify(Notification notification)
        {
            if (notification is Notification_Vehicle_Entity_Away)
            {
                controller.ChangeState(new State_Vehicle_Move());
            }
        }

        public override void OnUpdate()
        {
            entity.RigidBody.velocity = controller.Direction * Mathf.SmoothDamp(entity.RigidBody.velocity.magnitude, 0f, ref dampVelocity, 0.3f);
        }
    }
}