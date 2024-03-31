using Jam.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.StateMachine.Test
{
    /// <summary>
    /// Move to a random directly for [1, 2] second in set velocity.
    /// </summary>
    /// 
    public class State_Test_Move : State
    {
        private Vector2 direction;
        private float velocity = 3.0f;

        private float enteredTime;
        private float exitTime;

        public override void OnEnter(StateMachineController controller, Entity entity)
        {
            direction = Random.insideUnitCircle;

            enteredTime = Time.time;
            exitTime = Random.Range(1f, 2f);
        }

        public override void OnExit(StateMachineController controller, Entity entity)
        {
            entity.RigidBody.velocity = Vector2.zero;
        }

        public override void OnNotify(StateMachineController controller, Entity entity, Notification notification)
        {


        }

        public override void OnUpdate(StateMachineController controller, Entity entity)
        {
            
            entity.RigidBody.velocity = direction * velocity;
            if(Time.time - enteredTime >= exitTime)
            {
                // Change State
                controller.ChangeState(new State_Test_Idle());
            }

        }
    }

}