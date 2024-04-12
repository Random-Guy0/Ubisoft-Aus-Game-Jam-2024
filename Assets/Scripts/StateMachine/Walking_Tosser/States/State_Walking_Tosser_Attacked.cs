using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine.Generic;
using Jam.Entities;

namespace Jam.StateMachine.Walking_Tosser
{

    public class State_Walking_Tosser_Attacked : State<StateMachineController_Walking_Tosser, Entity>
    {
        float velocityMultiplier = 1.4f;
        float velocity;

        float panicDuration;

  

        float panicInterval = 0.15f;
        float panicTime = 0.0f;

        public override void OnEnter()
        {

            Vector2 dir = Random.insideUnitCircle.normalized;

            velocity = controller.Speed * velocityMultiplier;
            

            panicDuration = Random.Range(2.0f, 4.0f);

            
        }

        public override void OnExit()
        {
            entity.RigidBody.velocity = Vector2.zero;
        }

        public override void OnNotify(Notification notification)
        {
            if (notification is Notification_HealthZero)
            {
                controller.dizzyEffect.SetActive(true);
                controller.Tossed = true;
                controller.Multiplier = 1.8f;
                controller.ChangeState(new State_Walking_Tosser_Walk());
            }

        }

        public override void OnUpdate()
        {

            if(GetTime() > panicTime)
            {
                panicTime += panicInterval;
                Vector2 dir = Random.insideUnitCircle.normalized;
                entity.RigidBody.velocity = dir * velocity;
            }


            if(GetTime() > panicDuration)
            {
                controller.ChangeState(new State_Walking_Tosser_Walk());
            }
        }
    }

}

